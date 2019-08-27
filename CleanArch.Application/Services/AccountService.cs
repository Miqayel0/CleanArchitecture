using CleanArch.Application.Dtos.Account;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<RegisterOutDto> Register(RegisterDto input)
        {
            var user = new User { UserName = input.Email, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                return new RegisterOutDto { Code = code, UserId = user.Id };
            }
            else
            {
                throw new ApplicationException(string.Join('\n', result.Errors));
            }

        }

        public async Task ConfirmEmail(ConfirmEmailDto input)
        {
            var user = await _userManager.FindByIdAsync(input.UserId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{input.UserId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, input.Code);

            if (!result.Succeeded)
            {
                throw new ApplicationException(string.Join('\n', result.Errors));
            }
        }

        public async Task Login(LoginDto input)
        {
            //find user first...
            var user = await _userManager.FindByNameAsync(input.Email);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{user.Id}'."); ;
            }

            //validate password...
            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            //if password was ok, return this user.
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Invalid username or password"); ;
            }

        }

    }
}

