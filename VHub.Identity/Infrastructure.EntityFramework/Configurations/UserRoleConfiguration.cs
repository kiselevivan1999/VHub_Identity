using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("2c405bc9-8873-4491-b6af-537b901a8e52"),
                UserId = Guid.Parse("6ab774a9-d782-4223-999d-ffe4659eb780")
            },
            new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("2c405bc9-8873-4491-b6af-537b901a8e52"),
                UserId = Guid.Parse("473cb633-8f80-4910-bf3f-dd45383b28a6")
            });
    }
}
