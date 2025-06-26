using CustomCADs.Categories.Application.Categories.Commands.Internal.Delete;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Delete;

using static CategoriesData;

public class DeleteCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly DeleteCategoryHandler handler;
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IWrites<Category>> writes = new();
	private readonly Mock<ICategoryReads> reads = new();

	public DeleteCategoryHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		cache.Setup(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()))
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
		cache.Verify(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()), Times.Once());
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
