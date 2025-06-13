using CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit;

using static CategoriesData;

public class EditCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly EditCategoryHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Category category = CreateCategory();

	public EditCategoryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, raiser.Object);
		reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(category);
	}

	[Fact]
	public async Task Handler_ShouldQueryDatabase()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handler_ShouldPersistToDatabase()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handler_ShouldModifyCategory_WhenCategoryFound()
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
	public async Task Handler_ShouldRaiseEvents_WhenCategoryFound()
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(v => v.RaiseDomainEventAsync(
			It.Is<CategoryEditedDomainEvent>(x => x.Category.Name == ValidName && x.Category.Description == ValidDescription)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCategoryDoesNotExists()
	{
		// Arrange
		reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Category);
		EditCategoryCommand command = new(ValidId, new(ValidName, ValidDescription));

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Category>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
