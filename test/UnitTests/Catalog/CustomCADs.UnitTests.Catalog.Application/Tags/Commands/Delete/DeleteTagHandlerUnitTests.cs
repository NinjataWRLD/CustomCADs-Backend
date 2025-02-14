﻿using CustomCADs.Catalog.Application.Tags.Commands.Delete;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Catalog.Domain.Tags.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Delete;

public class DeleteTagHandlerUnitTests : TagsBaseUnitTests
{
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Tag>> writes = new();
    private readonly Mock<ITagReads> reads = new();

    private static readonly TagId id = new();
    private static readonly Tag tag = CreateTag();

    public DeleteTagHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(tag);
    }

    [Fact]
    public async Task Handler_ShouldQueryDatabase()
    {
        // Arrange
        DeleteTagCommand command = new(id);
        DeleteTagHandler handler = new(reads.Object, writes.Object, uow.Object);

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
        DeleteTagHandler handler = new(reads.Object, writes.Object, uow.Object);

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
        DeleteTagHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<TagNotFoundException>(async () =>
        {
            // Act  
            await handler.Handle(command, ct);
        });
    }
}
