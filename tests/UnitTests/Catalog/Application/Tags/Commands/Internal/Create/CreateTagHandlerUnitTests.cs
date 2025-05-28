using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create.Data;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create;

public class CreateTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly Mock<ITagWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	[Theory]
	[ClassData(typeof(CreateTagValidData))]
	public async Task Handler_ShouldPersistToDatabase(string name)
	{
		// Arrange
		CreateTagCommand command = new(name);
		CreateTagHandler handler = new(writes.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(v => v.AddAsync(
			It.Is<Tag>(x => x.Name == name),
			ct
		), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}
}
