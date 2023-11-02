using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.ViewModels.Articles;
using Blog.Entity.ViewModels.Categories;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public CategoryController(ICategoryService categoryService, IValidator<Category> validator, IMapper mapper, IToastNotification toastNotification)
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> DeletedCategory()
        {
            var categories = await categoryService.GetAllCategoriesDeleted();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddVM categoryAddVM)
        {
            var map = mapper.Map<Category>(categoryAddVM);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddVM);
                toastNotification.AddSuccessToastMessage(Messages.Category.AddSuccess(categoryAddVM.Name), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            else
            {
                result.AddModelState(this.ModelState);
                toastNotification.AddErrorToastMessage(Messages.Category.AddError(categoryAddVM.Name), new ToastrOptions() { Title = "Əməliyyat icra oluna bilmədi!" });
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddVM categoryAddVM)
        {
            var map = mapper.Map<Category>(categoryAddVM);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddVM);
                toastNotification.AddSuccessToastMessage(Messages.Category.AddSuccess(categoryAddVM.Name), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                return Json(Messages.Category.AddSuccess(categoryAddVM.Name));
            }

            else
            {
                toastNotification.AddErrorToastMessage(Messages.Category.AddError(categoryAddVM.Name), new ToastrOptions() { Title = "Əməliyyat icra oluna bilmədi!" });
                return Json(Messages.Category.AddError(categoryAddVM.Name));
            }

        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category = await categoryService.GetCategoryByGuid(categoryId);
            var map = mapper.Map<Category, CategoryUpdateVM>(category);
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVM categoryUpdateVM)
        {
            var map = mapper.Map<Category>(categoryUpdateVM);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
               var name =  await categoryService.UpdateCategoryAsync(categoryUpdateVM);
               toastNotification.AddSuccessToastMessage(Messages.Category.UpdateSuccess(categoryUpdateVM.Name), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
               return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            else
            {
                result.AddModelState(this.ModelState);
                toastNotification.AddErrorToastMessage(Messages.Category.UpdateError(categoryUpdateVM.Name), new ToastrOptions() { Title = "Əməliyyat icra oluna bilmədi!" });
            }

            return View();

        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await categoryService.SafeDeleteCategoryAsync(categoryId);
            toastNotification.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            var name = await categoryService.UndoDeleteCategoryAsync(categoryId);
            toastNotification.AddSuccessToastMessage(Messages.Category.UndoDelete(name), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }
    }
}
