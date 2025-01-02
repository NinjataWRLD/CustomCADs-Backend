using CustomCADs.Categories.Application.Categories.Commands.Delete;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Delete.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Delete;

using static CategoriesData;

public class DeleteCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Category>> writes = new();
    private readonly Mock<ICategoryReads> reads = new();

    public DeleteCategoryHandlerUnitTests()
    {
        reads.Setup(v => v.SingleByIdAsync(ValidId1, true, ct))
            .ReturnsAsync(CreateCategory(ValidId1, ValidName1, ValidDescription1));

        reads.Setup(v => v.SingleByIdAsync(ValidId2, true, ct))
            .ReturnsAsync(CreateCategory(ValidId2, ValidName2, ValidDescription2));
        
        reads.Setup(v => v.SingleByIdAsync(ValidId3, true, ct))
            .ReturnsAsync(CreateCategory(ValidId3, ValidName3, ValidDescription3));
    }

    [Theory]
    [ClassData(typeof(DeleteCategoryValidData))]
    public async Task Handler_ShouldQueryDatabase(CategoryId id)
    {
        // Arrange
        DeleteCategoryCommand command = new(id);
        DeleteCategoryHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(v => v.SingleByIdAsync(id, true, ct), Times.Once());
    }
    
    [Theory]
    [ClassData(typeof(DeleteCategoryValidData))]
    public async Task Handler_ShouldPersistToDatabase_WhenCategoryFound(CategoryId id)
    {
        // Arrange
        DeleteCategoryCommand command = new(id);
        DeleteCategoryHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(v => v.Remove(It.Is<Category>(x => x.Id == id)), Times.Once());
        uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
    }

    [Theory]
    [ClassData(typeof(DeleteCategoryValidData))]
    public async Task Handler_ShouldRaiseEvents(CategoryId id)
    {
        // Arrange
        DeleteCategoryCommand command = new(id);
        DeleteCategoryHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(v => v.RaiseDomainEventAsync(
            It.Is<CategoryDeletedDomainEvent>(x => x.Id == id)
        ), Times.Once());
    }

    [Theory]
    [ClassData(typeof(DeleteCategoryValidData))]
    public async Task Handle_ShouldThrowException_WhenCategoryNotFound(CategoryId id)
    {
        // Arrange
        reads.Setup(v => v.SingleByIdAsync(id, true, ct)).ReturnsAsync(null as Category);

        DeleteCategoryCommand command = new(id);
        DeleteCategoryHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<CategoryNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
