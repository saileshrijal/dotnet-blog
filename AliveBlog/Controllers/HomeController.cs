using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AliveBlog.Models;
using AliveBlog.Core.IConfiguration;
using AliveBlog.ViewModels;

namespace AliveBlog.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            RecentPosts = await _unitOfWork.Post.GetAllBlogs(),
            BannerPosts = await _unitOfWork.Post.GetAllBlogsForBanner()
        };
        return View(model);
    }

}
