using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category()
                {
                    Id = Guid.Parse("EAAFC77B-2856-4C7D-B725-31E23D04424B"),
                    Name = "Asp.Net Core MVC",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new Category()
                {
                    Id = Guid.Parse("D209510E-20B3-484A-A6D7-F6F6DD296FDF"),
                    Name = "C#",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });
        }
    }
}
