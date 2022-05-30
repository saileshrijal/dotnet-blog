using AliveBlog.Core.IConfiguration;
using AliveBlog.Models;
using AliveBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AliveBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHost;

        public PostController(IUnitOfWork unitOfWork,
                            UserManager<ApplicationUser> userManager,
                            IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var posts = await _unitOfWork.Post.GetAllWithCategories(userId);
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var CategoriesList = await _unitOfWork.Category.GetAll();
            var model = new PostViewModel();
            model.Categories = CategoriesList.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            var selectedCateogries = model.Categories.Where(x => x.Selected).Select(x => x.Value).Select(Guid.Parse).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var post = new Post
                    {
                        Title = model.Title,
                        Description = model.Description,
                        AuthorId = userId,
                        IsPublished = model.IsPublished
                    };
                    if (model.FeaturedPhoto != null)
                    {
                        post.FeaturedImageUrl = photoUpload(model);
                    }
                    if (model.Title != null)
                    {
                        string slug = model.Title.Trim();
                        slug.Replace(" ", "-");
                        post.Slug = slug + "_" + Guid.NewGuid();
                    }
                    post.PostCategories = new List<PostCategory>();
                    foreach (var categoryId in selectedCateogries)
                    {
                        post.PostCategories.Add(new PostCategory()
                        {
                            Post = post,
                            CategoryId = categoryId
                        });
                    }
                    await _unitOfWork.Post.Create(post);
                    await _unitOfWork.SaveAsync();
                    TempData["Success"] = "Post Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex;
                }
            }
            return View(model);
        }

        private string photoUpload(PostViewModel model)
        {
            string UniqueFileName = "";
            var folderPath = Path.Combine(_webHost.WebRootPath, "FeaturedImages");
            UniqueFileName = Guid.NewGuid() + "_" + model.FeaturedPhoto.FileName;
            var filePath = Path.Combine(folderPath, UniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.FeaturedPhoto.CopyTo(fileStream);
            }
            return UniqueFileName;
        }
    }
}