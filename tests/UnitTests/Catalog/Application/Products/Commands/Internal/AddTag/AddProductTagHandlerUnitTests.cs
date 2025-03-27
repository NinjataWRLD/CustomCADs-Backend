using CustomCADs.Catalog.Application.Products.Commands.Internal.AddTag;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.AddTag;

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
