﻿using Blog.Entity.Entities;
using Blog.Entity.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.ViewModels.Articles
{
    public class ArticleVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryVM Category { get; set; }
        public Image Image { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }
    }
}
