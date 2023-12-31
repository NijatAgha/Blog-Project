﻿using Blog.Entity.Entities;
using Blog.Entity.ViewModels.Articles;
using Blog.Entity.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllCategoriesNonDeleted();
        Task<List<CategoryVM>> GetAllCategoriesNonDeletedTake24();
        Task<List<CategoryVM>> GetAllCategoriesDeleted();
        Task CreateCategoryAsync(CategoryAddVM categoryAddVM);
        Task<Category> GetCategoryByGuid(Guid id);
        Task<string> UpdateCategoryAsync(CategoryUpdateVM categoryUpdateVM);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);
        Task<string> UndoDeleteCategoryAsync(Guid categoryId);
    }
}
