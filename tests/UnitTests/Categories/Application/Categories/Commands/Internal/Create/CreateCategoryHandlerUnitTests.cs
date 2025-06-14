using CustomCADs.Categories.Application.Categories.Commands.Internal.Create;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Events;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;

using static CategoriesData;

public class CreateCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly CreateCategoryHandler handler;
	private readonly Mock<IWrites<Category>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	public CreateCategoryHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, raiser.Object);
	}

	[Fact]
	public async Task Handler_ShouldPersistToDatabase()
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
	public async Task Handler_ShouldRaiseEvents()
	{
		// Arrange
		CreateCategoryCommand command = new(Dto: new(ValidName, ValidDescription));

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(v => v.RaiseDomainEventAsync(
			It.Is<CategoryCreatedDomainEvent>(x => x.Category.Name == ValidName && x.Category.Description == ValidDescription)
		), Times.Once());
	}
}
