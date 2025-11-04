using AutoFixture;
using AutoFixture.AutoMoq;

namespace Application.Tests.AutoFixtureCustomizations;

/// <summary>
/// Кастомизация для генерации ролей
/// </summary>
internal class RoleServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }
}
