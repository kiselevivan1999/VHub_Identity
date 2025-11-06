using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User("Ivan", "Ivan@gmail.com")
            {
                Id = Guid.Parse("6ab774a9-d782-4223-999d-ffe4659eb780"),
                NormalizedUserName = "IVAN",
                NormalizedEmail = "IVAN@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEJclNaSM+VykPmT4NxD2EXDRjW2KXy8DYo8+9UDEtBJAsUzrIkICLjG7HHYWI8hJNQ==",
                SecurityStamp = "F7VJLSKLX3FVHCCBMCSCU7XKTTC4WZKZ",
                ConcurrencyStamp = "b507ce46-bf48-412b-b1c1-4187cd5940b7",
                LockoutEnabled = true
            },
            new User("Roman", "Roman@gmail.com")
            {
                Id = Guid.Parse("473cb633-8f80-4910-bf3f-dd45383b28a6"),
                NormalizedUserName = "ROMAN",
                NormalizedEmail = "ROMAN@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEJclNaSM+VykPmT4NxD2EXDRjW2KXy8DYo8+9UDEtBJAsUzrIkICLjG7HHYWI8hJNQ==",
                SecurityStamp = "F7VJLSKLX3FVHCCBMCSCU7XKTTC4WZKZ",
                ConcurrencyStamp = "b507ce46-bf48-412b-b1c1-4187cd5940b7",
                LockoutEnabled = true
            }
            );
    }
}
