using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
                new AppUserRole()
                {
                    UserId = Guid.Parse("B61DEC0E-E367-4818-A435-82305C7C7FB7"),
                    RoleId = Guid.Parse("0DCFD34B-619B-47A9-84B8-B19BFAE0B443")

                },
                new AppUserRole()
                {
                    UserId = Guid.Parse("C9728321-B41D-4DFE-AA9E-F80A807F642F"),
                    RoleId = Guid.Parse("F661DC69-44A9-40FD-B298-91A73B7DECBD")
                });
        }
    }
}
