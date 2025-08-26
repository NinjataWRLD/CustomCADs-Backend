using CustomCADs.Catalog.Application.Categories.Commands.Internal.Delete;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Commands.Internal.Delete;

using static CategoriesData;

public class DeleteCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly DeleteCategoryHandler handler;
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<ICategoryWrites> writes = new();
	private readonly Mock<ICategoryReads> reads = new();

	public DeleteCategoryHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateCategory(ValidId, ValidName, ValidDescription));
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		DeleteCategoryCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteCategoryCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(v => v.Remove(
			It.Is<Category>(x => x.Id == ValidId)
		), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldClearCache()
	{
		// Arrange
		DeleteCategoryCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.ClearAsync(ValidId));
	}
}
