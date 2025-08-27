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
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private static readonly Tag tag = CreateTag();

	public EditTagHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(tag);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditTagCommand command = new(ValidId, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		EditTagCommand command = new(ValidId, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, tag),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditTagCommand command = new(ValidId, MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}


	[Fact]
	public async Task Handle_ShouldThrowException_WhenTagNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Tag);
		EditTagCommand command = new(ValidId, MaxValidName);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
