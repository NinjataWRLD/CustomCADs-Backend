using CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;

using static TagsData;

public class EditTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly EditTagHandler handler;
	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly TagId id = new();
	private static readonly Tag tag = CreateTag();

	public EditTagHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(tag);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditTagCommand command = new(id, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(id, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditTagCommand command = new(id, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(id, true, ct), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}


	[Fact]
	public async Task Handle_ShouldThrowException_WhenTagNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Tag);
		EditTagCommand command = new(id, MaxValidName);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
