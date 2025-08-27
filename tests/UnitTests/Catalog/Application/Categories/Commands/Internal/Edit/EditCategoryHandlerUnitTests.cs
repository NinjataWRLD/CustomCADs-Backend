using CustomCADs.Catalog.Application.Categories.Commands.Internal.Edit;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Commands.Internal.Edit;

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
		reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(category);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
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
