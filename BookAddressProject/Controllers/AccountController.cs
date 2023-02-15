using BookAddressProject.Controllers;
using BookAddressProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<IdentityUser>? userManager;
        private readonly SignInManager<IdentityUser>? signInManager;
        public AccountController(UserManager<IdentityUser> _userManager,
                                 SignInManager<IdentityUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        [HttpPost("RegisterViewModel")]
        public async Task<IActionResult> RegisterViewModel([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var user = new IdentityUser { Email = model.Email };
                var result = await userManager!.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager!.SignInAsync(user, false);
                    return Ok();
                }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await signInManager!.PasswordSignInAsync( model.Email, model.Password,model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await userManager!.FindByEmailAsync(model.Email);
                    return Ok(user);
                }
            if (result.IsLockedOut)
            {
                return Unauthorized("User is locked out.");
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager!.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}
