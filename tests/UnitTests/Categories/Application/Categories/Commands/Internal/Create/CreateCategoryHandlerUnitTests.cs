using CustomCADs.Categories.Application.Categories.Commands.Internal.Create;
using CustomCADs.Categories.Domain.Repositories;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;

using static CategoriesData;

public class CreateCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly CreateCategoryHandler handler;
	private readonly Mock<IWrites<Category>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	public CreateCategoryHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object);

		writes.Setup(v => v.AddAsync(
			It.Is<Category>(x => x.Name == ValidName && x.Description == ValidDescription),
			ct
		)).ReturnsAsync(CreateCategory(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateCategoryCommand command = new(Dto: new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(v => v.AddAsync(
			It.Is<Category>(x => x.Name == ValidName && x.Description == ValidDescription),
			ct
		), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		CreateCategoryCommand command = new(Dto: new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(v => v.UpdateAsync(
			ValidId,
			It.Is<Category>(x => x.Name == ValidName && x.Description == ValidDescription)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateCategoryCommand command = new(Dto: new(ValidName, ValidDescription));

		// Act
		CategoryId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
