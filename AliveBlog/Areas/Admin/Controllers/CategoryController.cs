using AliveBlog.Core.IConfiguration;
using AliveBlog.Models;
using AliveBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AliveBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _unitOfWork.Category.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = new Category
                    {
                        Title = model.Title,
                        Description = model.Description
                    };
                    await _unitOfWork.Category.Create(category);
                    await _unitOfWork.SaveAsync();
                    TempData["Success"] = "Category Created Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return PartialView("_NotFoundAdmin");
            }
            var category = await _unitOfWork.Category.GetBy(c => c.Id == id);

            if (category == null)
            {
                return PartialView("_NotFoundAdmin");
            }
            var model = new CategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = new Category
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description
                    };
                    _unitOfWork.Category.Edit(category);
                    await _unitOfWork.SaveAsync();
                    TempData["Success"] = "Category Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex;
                }
            }
            return View(model);
        }
    }
}