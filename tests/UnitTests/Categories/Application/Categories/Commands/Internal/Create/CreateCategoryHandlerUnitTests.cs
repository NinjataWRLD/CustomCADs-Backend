using CustomCADs.Categories.Application.Categories.Commands.Internal.Create;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;

public class CreateCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly CreateCategoryHandler handler;
    private readonly Mock<IWrites<Category>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IEventRaiser> raiser = new();

    public CreateCategoryHandlerUnitTests()
    {
        handler = new(writes.Object, uow.Object, raiser.Object);
    }

    [Theory]
    [ClassData(typeof(CreateCategoryValidData))]
    public async Task Handler_ShouldPersistToDatabase(string name, string description)
    {
        // Arrange
        CreateCategoryCommand command = new(Dto: new(name, description));

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(v => v.AddAsync(
            It.Is<Category>(x => x.Name == name && x.Description == description),
            ct
        ), Times.Once());
        uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
    }

    [Theory]
    [ClassData(typeof(CreateCategoryValidData))]
    public async Task Handler_ShouldRaiseEvents(string name, string description)
    {
        // Arrange
        CreateCategoryCommand command = new(Dto: new(name, description));

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(v => v.RaiseDomainEventAsync(
            It.Is<CategoryCreatedDomainEvent>(x => x.Category.Name == name && x.Category.Description == description)
        ), Times.Once());
    }
}
