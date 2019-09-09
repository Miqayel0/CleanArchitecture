using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Mapper;
using CleanArch.Domain.Entities;
using CleanArch.infra.IoC;
using CleanArch.Infra.Auth.Constants;
using CleanArch.Infra.Auth.Options;
using CleanArch.Infra.Auth.Services;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace CleanArch.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builderr => builderr.AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials()
                                        .WithOrigins("http://localhost:3000")
                                        .WithOrigins("http://localhost:3001"));
            });

            services.AddMemoryCache();

            var authSettings = Configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            }).AddOpenIdConnect("Google", o =>
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                o.ClientId = googleAuthNSection["ClientId"];
                o.ClientSecret = googleAuthNSection["ClientSecret"];
                o.Authority = "https://accounts.google.com";
                o.ResponseType = OpenIdConnectResponseType.Code;
                o.CallbackPath = "/signin-google"; // Or register the default "/sigin-oidc"
                o.Scope.Add("email");
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //.AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection =
            //        Configuration.GetSection("Authentication:Google");

            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //});

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));
            });

            // add identity
            var identityBuilder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.SignIn.RequireConfirmedEmail = true;
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddRoleValidator<RoleValidator<IdentityRole>>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddSignInManager<SignInManager<User>>();
            identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IEmailSender, EmailSenderService>();

            services.AddMediatR(typeof(Startup));

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CleanArch", Version = "v1" });
                // Swagger 2.+ support
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArch V1");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
    }
}
