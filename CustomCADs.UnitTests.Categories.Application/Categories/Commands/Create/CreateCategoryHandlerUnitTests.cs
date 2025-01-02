using CustomCADs.Categories.Application.Categories.Commands.Create;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Create.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Create;

public class CreateCategroyHandlerData : TheoryData<string, string>;

public class CreateCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Category>> writes = new();

    [Theory]
    [ClassData(typeof(EditCategoryHandlerValidData))]
    public async Task Handler_ShouldPersistToDatabase(string name, string description)
    {
        // Arrange
        CategoryWriteDto dto = new(name, description);
        CreateCategoryCommand command = new(dto);
        CreateCategoryHandler handler = new(writes.Object, uow.Object, raiser.Object);

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
    [ClassData(typeof(EditCategoryHandlerValidData))]
    public async Task Handler_ShouldRaiseEvents(string name, string description)
    {
        // Arrange
        CategoryWriteDto dto = new(name, description);
        CreateCategoryCommand command = new(dto);
        CreateCategoryHandler handler = new(writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(v => v .RaiseDomainEventAsync(
            It.Is<CategoryCreatedDomainEvent>(x => x.Category.Name == name && x.Category.Description == description)
        ), Times.Once());
    }
}
