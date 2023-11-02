using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.ViewModels.Articles;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstracts;
using Blog.Web.Consts;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toastNotification;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator, IToastNotification toastNotification)
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNonDeletedsAsync();
            return View(articles);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> DeletedArticle()
        {
            var articles = await articleService.GetAllArticlesWithCategoryDeletedAsync();
            return View(articles);
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddVM() { Categories = categories});
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Add(ArticleAddVM articleAddVM)
        {
            var map = mapper.Map<Article>(articleAddVM);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await articleService.CreateArticleAsync(articleAddVM);
                toastNotification.AddSuccessToastMessage(Messages.Article.AddSuccess(articleAddVM.Title), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }

            else
            {
                toastNotification.AddErrorToastMessage(Messages.Article.AddError(articleAddVM.Title), new ToastrOptions() { Title = "Əməliyyat icra oluna bilmədi!" });
                result.AddModelState(this.ModelState);
            }

            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddVM() { Categories = categories });
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid articleId)
        {
            var article = await articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            var articleUpdateVM = mapper.Map<ArticleUpdateVM>(article);
            articleUpdateVM.Categories = categories;
            return View(articleUpdateVM);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Update(ArticleUpdateVM articleUpdateVM)
        {
            var map = mapper.Map<Article>(articleUpdateVM);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var title = await articleService.UpdateArticleAsync(articleUpdateVM);
                toastNotification.AddSuccessToastMessage(Messages.Article.UpdateSuccess(title), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });

            }

            else
            {
                toastNotification.AddErrorToastMessage(Messages.Article.UpdateError(articleUpdateVM.Title), new ToastrOptions() { Title = "Əməliyyat icra oluna bilmədi!" });
                result.AddModelState(this.ModelState);
            }
     

            var categories = await categoryService.GetAllCategoriesNonDeleted();
            articleUpdateVM.Categories = categories;
            return View(articleUpdateVM);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid articleId)
        {
           var title = await articleService.SafeDeleteArticleAsync(articleId);
           toastNotification.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
           return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
        
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> UndoDelete(Guid articleId)
        {
           var title = await articleService.UndoDeleteArticleAsync(articleId);
           toastNotification.AddSuccessToastMessage(Messages.Article.UndoDelete(title), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
           return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
    }
}
