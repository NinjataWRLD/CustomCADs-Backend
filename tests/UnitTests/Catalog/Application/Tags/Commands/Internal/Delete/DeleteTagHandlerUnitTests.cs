using CustomCADs.Catalog.Application.Tags.Commands.Internal.Delete;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Delete;

using static TagsData;

public class DeleteTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly DeleteTagHandler handler;
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<ITagWrites> writes = new();
	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private static readonly Tag tag = CreateTag();

	public DeleteTagHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(tag);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteTagCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		DeleteTagCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteTagCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(v => v.Remove(tag), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenTagNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Tag);
		DeleteTagCommand command = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
