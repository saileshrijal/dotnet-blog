using AliveBlog.Core.IConfiguration;
using AliveBlog.Models;
using AliveBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AliveBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHost;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHost, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _webHost = webHost;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    };

                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                    }
                    if (!await _roleManager.RoleExistsAsync("Author"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole { Name = "Author" });
                    }
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (model.IsAdmin)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "Author");
                        }
                        TempData["Success"] = "User Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            var model = new UserViewModel
            {
                UserId = CurrentUser.Id,
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                Email = CurrentUser.Email,
                Password = CurrentUser.PasswordHash,
                ProfilePictureUrl = CurrentUser.ProfilePictureUrl,
                AboutMe = CurrentUser.AboutMe,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.UserId);

                user.Id = model.UserId;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.AboutMe = model.AboutMe;

                if (model.ProfilePicture == null)
                {
                    user.ProfilePictureUrl = model.ProfilePictureUrl;
                }
                else
                {
                    user.ProfilePictureUrl = photoUpload(model);
                }
                await _userManager.UpdateAsync(user);
                TempData["Success"] = "Profile Updated Successfully";
                return RedirectToAction(nameof(Profile));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            TempData["Success"] = "You are logged out Succesfully";
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    TempData["Success"] = "You are logged in successfully";
                    return RedirectToAction(nameof(Index), "Dashboard", new { area = "Admin" });
                }
                else
                {
                    TempData["Error"] = "Incorrect UserName or Password";
                    return View(model);
                }
            }
            return View(model);
        }

        private string photoUpload(UserViewModel model)
        {
            string UniqueFileName = "";
            var folderPath = Path.Combine(_webHost.WebRootPath, "UserProfileImages");
            UniqueFileName = model.UserId + "_" + model.ProfilePicture.FileName;
            var filePath = Path.Combine(folderPath, UniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ProfilePicture.CopyTo(fileStream);
            }
            return UniqueFileName;
        }
    }
}