using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CleanArch.Application.Dtos.Account;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IAccountService _accountService;

        public AccountController(IEmailSender emailSender, IAccountService accountService)
        {
            _emailSender = emailSender;
            _accountService = accountService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto input)
        {
            var result = await _accountService.Register(input);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", values: new { userId = result.UserId, code = result.Code }, protocol: Request.Scheme, "localhost:5001");
            await _emailSender.SendEmailAsync(input.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return Ok();
        }

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            await _accountService.ConfirmEmail(new ConfirmEmailDto { Code = code, UserId = userId });

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AccountDto>> Login(LoginDto input)
        {
            return Ok(await _accountService.Login(input));
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
