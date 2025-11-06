using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role 
            {
                Id = Guid.Parse("2c405bc9-8873-4491-b6af-537b901a8e52"),
                Name = "admin",
                NormalizedName = "ADMIN"
            });
    }
}
