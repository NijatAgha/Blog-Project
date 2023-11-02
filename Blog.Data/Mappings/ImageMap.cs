using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(
                new Image()
                {
                    Id = Guid.Parse("29D4EAC9-05BF-44B2-B55C-2B3ED3DFF490"),
                    FileName = "images/testimage",
                    FileType = "jpg",
                    IsDeleted = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                },
                new Image()
                {
                    Id = Guid.Parse("E264A762-C52D-4CEE-9301-9E939EEF39B9"),
                    FileName = "images/testimage2",
                    FileType = "png",
                    IsDeleted = false,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                });
        }
    }
}
