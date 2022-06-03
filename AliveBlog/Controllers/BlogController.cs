using AliveBlog.Core.IConfiguration;
using AliveBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AliveBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _unitOfWork.Post.GetAllBlogs();
            return View(posts);
        }

        [Route("Post/{slug}")]
        public async Task<IActionResult> Post(string? slug)
        {
            if (slug == "" || slug == null)
            {
                return PartialView("_NotFoundBlog");
            }
            var post = await _unitOfWork.Post.GetWithCategoriesBy(p => p.Slug == slug);

            if (post == null)
            {
                return PartialView("_NotFoundBlog");
            }
            return View(post);
        }

        [Route("Category/{category}")]
        public async Task<IActionResult> Category(string? category)
        {
            if (category == null)
            {
                return PartialView("_NotFoundBlog");
            }
            var posts = await _unitOfWork.PostCategory.GetAllBlogForCategory(category);
            ViewData["Category"] = category;
            return View(posts);
        }
    }
}