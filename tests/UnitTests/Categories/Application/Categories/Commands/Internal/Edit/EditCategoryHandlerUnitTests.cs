using CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit;

using static CategoriesData;

public class EditCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly EditCategoryHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	private readonly Category category = CreateCategory();

	public EditCategoryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);
		cache.Setup(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>())).ReturnsAsync(category);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldModifyCategory()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.Equal(ValidName, category.Name);
			Assert.Equal(ValidDescription, category.Description);
		});
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(v => v.UpdateAsync(
			ValidId,
			category
		), Times.Once());
	}
}
