using Blog.Entity.ViewModels.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.ViewModels.Articles
{
    public class ArticleAddVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Photo { get; set; }
        public Guid CategoryId { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
