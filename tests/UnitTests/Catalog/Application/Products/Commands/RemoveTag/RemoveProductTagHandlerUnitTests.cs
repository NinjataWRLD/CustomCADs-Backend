using CustomCADs.Catalog.Application.Products.Commands.RemoveTag;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.RemoveTag;

using static ProductsData;

public class RemoveProductTagHandlerUnitTests
{
    private readonly Mock<IProductWrites> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private readonly static ProductId id = ValidId;
    private readonly static TagId tagId = TagId.New();

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        RemoveProductTagCommand command = new(id, tagId);
        RemoveProductTagHandler handler = new(writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.RemoveTag(id, tagId), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
