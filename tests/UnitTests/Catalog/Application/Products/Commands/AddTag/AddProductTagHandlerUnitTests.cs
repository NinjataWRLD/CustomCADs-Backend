﻿using CustomCADs.Catalog.Application.Products.Commands.AddTag;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Writes;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.AddTag;

using static ProductsData;

public class AddProductTagHandlerUnitTests
{
    private readonly Mock<IProductWrites> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private readonly static ProductId id = ValidId;
    private readonly static TagId tagId = TagId.New();

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        AddProductTagCommand command = new(id, tagId);
        AddProductTagHandler handler = new(writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddTagAsync(id, tagId, ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
