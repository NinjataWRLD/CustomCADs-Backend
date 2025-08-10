using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create;

using static TagsData;

public class CreateTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly CreateTagHandler handler;
	private readonly Mock<ITagWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateTagHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(v => v.AddAsync(
			It.Is<Tag>(x => x.Name == MaxValidName),
			ct
		)).ReturnsAsync(CreateTag(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateTagCommand command = new(MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(v => v.AddAsync(
			It.Is<Tag>(x => x.Name == MaxValidName),
			ct
		), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateTagCommand command = new(MaxValidName);

		// Act
		TagId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
