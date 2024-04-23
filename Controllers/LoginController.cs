using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models.ViewModels;
using System.Threading.Tasks;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using FinalProject.Repositories;

namespace FinalProject.Controllers
{
    public class LoginController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;

            _passwordHasher = passwordHasher;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        //        var user = await _userRepository.GetUserByUsernameAsync(model.Username);

        //        if (user != null)
        //        {
        //            var result = _passwordHasher.VerifyHashedPassword(user, _passwordHasher.HashPassword(user,user.Password), model.Password);


        //            if (result == PasswordVerificationResult.Success)
        //            {
        //                await SignInUser(user);
        //                if (user.Role == "Admin") {
        //                }
        //                else {
        //                    return RedirectToAction("Index", "Home");

        //                }
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //    }
        //    return View(model);
        //}
        //
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByUsernameAsync(model.Username);
                if (user != null && await _userRepository.VerifyUserPasswordAsync(user, model.Password))
                {
                    await SignInUser(user);
                    return RedirectToAction("Index", "Session"); 
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
        {   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
        };
            Console.Out.WriteLine("john");
            Console.Out.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n" + User.FindFirstValue(ClaimTypes.NameIdentifier)+"\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            var authProperties = new AuthenticationProperties
            {
                
            };

            await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return RedirectToAction("Login");
        }
    }
}