using AutoFixture;
using AutoFixture.Xunit2;

namespace Application.Tests.AutoFixtureCustomizations;

public class RoleServiceAutoDataAttribute : AutoDataAttribute
{
    public RoleServiceAutoDataAttribute() : base(() =>
        new Fixture().Customize(new RoleServiceCustomization()))
    {
    }
}
