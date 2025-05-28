using CustomCADs.Catalog.Application.Tags.Commands.Internal.Delete;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Delete;

public class DeleteTagHandlerUnitTests : TagsBaseUnitTests
{
    private readonly DeleteTagHandler handler;
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<ITagWrites> writes = new();
    private readonly Mock<ITagReads> reads = new();

    private static readonly TagId id = new();
    private static readonly Tag tag = CreateTag();

    public DeleteTagHandlerUnitTests()
    {
        handler = new(reads.Object, writes.Object, uow.Object);

        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(tag);
    }

    [Fact]
    public async Task Handler_ShouldQueryDatabase()
    {
        // Arrange
        DeleteTagCommand command = new(id);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(v => v.SingleByIdAsync(id, true, ct), Times.Once());
    }

    [Fact]
    public async Task Handler_ShouldPersistToDatabase()
    {
        // Arrange
        DeleteTagCommand command = new(id);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(v => v.Remove(tag), Times.Once());
        uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenTagDoesNotExist()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as Tag);
        DeleteTagCommand command = new(id);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(
            // Act  
            async () => await handler.Handle(command, ct)
        );
    }
}
