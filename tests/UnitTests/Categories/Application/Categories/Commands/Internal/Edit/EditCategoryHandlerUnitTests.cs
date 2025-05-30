using CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit;

using Data;
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

	[Theory]
	[ClassData(typeof(EditCategoryValidData))]
	public async Task Handler_ShouldQueryDatabase(string name, string description)
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(name, description));

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Theory]
	[ClassData(typeof(EditCategoryValidData))]
	public async Task Handler_ShouldPersistToDatabase(string name, string description)
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(name, description));

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Theory]
	[ClassData(typeof(EditCategoryValidData))]
	public async Task Handler_ShouldModifyCategory_WhenCategoryFound(string name, string description)
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(name, description));

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
	[ClassData(typeof(EditCategoryValidData))]
	public async Task Handler_ShouldRaiseEvents_WhenCategoryFound(string name, string description)
	{
		// Arrange
		EditCategoryCommand command = new(ValidId, new(name, description));

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(v => v.RaiseDomainEventAsync(
			It.Is<CategoryEditedDomainEvent>(x => x.Category.Name == name && x.Category.Description == description)
		), Times.Once());
	}

	[Theory]
	[ClassData(typeof(EditCategoryValidData))]
	public async Task Handle_ShouldThrowException_WhenCategoryDoesNotExists(string name, string description)
	{
		// Arrange
		reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Category);
		EditCategoryCommand command = new(ValidId, new(name, description));

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Category>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
