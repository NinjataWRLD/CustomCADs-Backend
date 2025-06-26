using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Domain.Tags;

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
}
