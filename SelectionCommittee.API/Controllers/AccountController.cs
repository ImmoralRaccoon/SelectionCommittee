using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Authentication;
using SelectionCommittee.Authentication.Services;

namespace SelectionCommittee.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthentificationService _authentificationService;

        public AccountController(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        [HttpPost("LogIn")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LogIn([FromBody] UserLoginModel model)
        {
            var logInSuccess = await _authentificationService.LogIn(model.Email, model.Password);
            if (!logInSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("LogOut")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LogOut()
        {
            await _authentificationService.LogOut();
            return Ok();
        }

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var identityErrors =
                await _authentificationService.Register(model.Email, model.Password, model.PasswordConfirm, model.Roles);
            if (identityErrors.Count() != 0)
            {
                return BadRequest(identityErrors);
            }
            return Ok();
        }
    }
}