using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(
                new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "Asp.Net Core MVC Article 1",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    ViewCount = 15,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = Guid.Parse("EAAFC77B-2856-4C7D-B725-31E23D04424B"),
                    ImageId = Guid.Parse("29D4EAC9-05BF-44B2-B55C-2B3ED3DFF490"),
                    UserId = Guid.Parse("B61DEC0E-E367-4818-A435-82305C7C7FB7")
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "C# Article 1",
                    Content = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish.",
                    ViewCount = 20,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = Guid.Parse("D209510E-20B3-484A-A6D7-F6F6DD296FDF"),
                    ImageId = Guid.Parse("E264A762-C52D-4CEE-9301-9E939EEF39B9"),
                    UserId = Guid.Parse("C9728321-B41D-4DFE-AA9E-F80A807F642F")
                });
        }
    }
}
