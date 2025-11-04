using Application.Implementations.Services;
using Application.Tests.AutoFixtureCustomizations;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.Tests.Services;

//TODO: Пересмотреть структуру класса и наименование методов.
public class RoleServiceTests
{
    private readonly IFixture _fixture;

    public RoleServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    // ТЕСТ 1: Успешное создание роли
    [Theory]
    [RoleServiceAutoData]
    public async Task Create_WithValidName_ShouldReturnRoleId(
        [Frozen(Matching.ImplementedInterfaces)] Mock<RoleManager<Role>> roleManagerMock,
        string roleName)
    {
        // Arrange
        var createdRoleId = _fixture.Create<Guid>();

        roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<Role>()))
            .Callback<Role>(role => role.Id = createdRoleId)
            .ReturnsAsync(IdentityResult.Success);

        //Создаю в обход AutoFixture, потому что он тупит
        var service = new RoleService(roleManagerMock.Object);

        // Act
        var result = await service.Create(roleName);

        // Assert
        result.Should().Be(createdRoleId);
        roleManagerMock.Verify(x => x.CreateAsync(It.Is<Role>(r =>
            r.Name == roleName && r.NormalizedName == roleName.ToUpper())),
            Times.Once);
    }

    // ТЕСТ 2: Ошибка при создании роли
    [Theory]
    [RoleServiceAutoData]
    public async Task Create_WhenCreationFails_ShouldThrowBadRequestException(
        [Frozen] Mock<RoleManager<Role>> roleManagerMock,
        RoleService service,
        string roleName)
    {
        // Arrange
        var errors = new[]
        {
            new IdentityError { Description = "Тестовая ошибка" }
        };

        roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<Role>()))
            .ReturnsAsync(IdentityResult.Failed(errors));

        // Act & Assert
        var act = () => service.Create(roleName);

        // Assert
        var exception = await act.Should().ThrowAsync<BadRequestException>();
    }

    // ТЕСТ 3: Получение всех ролей
    [Fact]
    public async Task GetAll_WhenRolesExist_ShouldReturnAllRoles()
    {
        // Arrange
        var storeMock = new Mock<IQueryableRoleStore<Role>>();
        storeMock.As<IRoleStore<Role>>();
    
        var roleManagerMock = new Mock<RoleManager<Role>>(storeMock.Object, null, null, null, null);
        var service = new RoleService(roleManagerMock.Object);
    
        var testRoles = new List<Role>
        {
            new Role { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" }
        };
        roleManagerMock.Setup(x => x.Roles)
            .Returns(testRoles.AsQueryable());

        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should().HaveCount(3);
    }

    // ТЕСТ 4: Получение ролей когда их нет
    [Theory]
    [RoleServiceAutoData]
    public async Task GetAll_WhenNoRoles_ShouldReturnEmptyArray(
        [Frozen] Mock<RoleManager<Role>> roleManagerMock,
        RoleService service)
    {
        // Arrange
        var emptyRoles = Enumerable.Empty<Role>().AsQueryable();
        roleManagerMock.Setup(x => x.Roles).Returns(emptyRoles);

        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    // ТЕСТ 5: Успешное удаление роли
    [Theory]
    [RoleServiceAutoData]
    public async Task Delete_WhenRoleExists_ShouldReturnTrue(
        [Frozen] Mock<RoleManager<Role>> roleManagerMock,
        RoleService service,
        Guid roleId)
    {
        // Arrange
        //TODO: Мб автоматизировать, чтобы сразу подхватывать параметры, без With()
        var testRole = _fixture.Build<Role>()
            .With(r => r.Id, roleId)
            .Create();

        var testRoles = new List<Role> { testRole };

        roleManagerMock.Setup(x => x.Roles).Returns(testRoles.AsQueryable());
        roleManagerMock.Setup(x => x.DeleteAsync(It.IsAny<Role>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await service.Delete(roleId, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        roleManagerMock.Verify(x => x.DeleteAsync(It.Is<Role>(r =>
            r.Id == roleId)), Times.Once);
    }

    // ТЕСТ 6: Ошибка при удалении несуществующей роли
    [Theory]
    [RoleServiceAutoData]
    public async Task Delete_WhenRoleNotFound_ShouldThrowNotFoundException(
        [Frozen] Mock<RoleManager<Role>> roleManagerMock,
        RoleService service,
        Guid nonExistentRoleId)
    {
        // Arrange
        var existingRoles = _fixture.CreateMany<Role>(2).ToList();
        roleManagerMock.Setup(x => x.Roles).Returns(existingRoles.AsQueryable());

        // Act & Assert
        var act = () => service.Delete(nonExistentRoleId, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>();
    }

    // ТЕСТ 7: Ошибка при неудачном удалении
    [Theory]
    [RoleServiceAutoData]
    public async Task Delete_WhenDeletionFails_ShouldThrowBadRequestException(
        [Frozen] Mock<RoleManager<Role>> roleManagerMock,
        RoleService service,
        Guid roleId)
    {
        // Arrange
        var testRole = _fixture.Build<Role>()
            .With(r => r.Id, roleId)
            .Create();

        var testRoles = new List<Role> { testRole };
        var errors = new[] { new IdentityError { Description = "Тестовая ошибка" } };

        roleManagerMock.Setup(x => x.Roles).Returns(testRoles.AsQueryable());
        roleManagerMock.Setup(x => x.DeleteAsync(It.IsAny<Role>()))
            .ReturnsAsync(IdentityResult.Failed(errors));

        // Act & Assert
        var act = () => service.Delete(roleId, CancellationToken.None);
        await act.Should().ThrowAsync<BadRequestException>();
    }
}