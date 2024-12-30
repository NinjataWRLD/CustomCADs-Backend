using CustomCADs.Categories.Application.Categories.Commands.Edit;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Create.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Edit;

using static CategoriesData;

public class EditCategoryHandlerData : TheoryData<string, string>;

public class EditCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<ICategoryReads> reads = new();
    private readonly Category category = CreateCategory();

    public EditCategoryHandlerUnitTests()
    {
        reads.Setup(v => v.SingleByIdAsync(ValidId1, true, ct))
            .ReturnsAsync(category);
    }

    [Theory]
    [ClassData(typeof(EditCategoryHandlerValidData))]
    public async Task Handler_ShouldModifyCategory(string name, string description)
    {
        // Arrange
        EditCategoryCommand command = new(ValidId1, new(name, description));
        EditCategoryHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.Equal(category.Name, name);
            Assert.Equal(category.Description, description);
        });
    }

    [Theory]
    [ClassData(typeof(EditCategoryHandlerValidData))]
    public async Task Handler_ShouldCallDatabase(string name, string description)
    {
        // Arrange
        EditCategoryCommand command = new(ValidId1, new(name, description));
        EditCategoryHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(v => v.SingleByIdAsync(ValidId1, true, ct), Times.Once());
        uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
    }

    [Theory]
    [ClassData(typeof(EditCategoryHandlerValidData))]
    public async Task Handler_ShouldRaiseEvents(string name, string description)
    {
        // Arrange
        EditCategoryCommand command = new(ValidId1, new(name, description));
        EditCategoryHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(v => v.RaiseDomainEventAsync(
            It.Is<CategoryEditedDomainEvent>(x => x.Category.Name == name && x.Category.Description == description)
        ), Times.Once());
    }

    [Theory]
    [ClassData(typeof(EditCategoryHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenCategoryDoesNotExists(string name, string description)
    {
        // Arrange
        CategoryId id = ValidId1;
        reads.Setup(v => v.SingleByIdAsync(id, true, ct)).ReturnsAsync(null as Category);

        EditCategoryCommand command = new(id, new(name, description));
        EditCategoryHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<CategoryNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
