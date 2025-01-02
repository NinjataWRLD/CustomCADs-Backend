using CustomCADs.Files.Application.Cads.IntegrationEventHandlers;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.UnitTests.Files.Application.Cads.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandlerUnitTests : CadsBaseUnitTests
{
    private readonly ICadReads reads = Substitute.For<ICadReads>();
    private readonly IWrites<Cad> writes = Substitute.For<IWrites<Cad>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IStorageService storage = Substitute.For<IStorageService>();
    private static readonly Cad cad = CreateCad();

    public ProductDeletedIntegrationEventHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, true, ct).Returns(cad);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: default,
            CadId: id
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Act
        await handler.Handle(ie);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, true, ct);
    }
    
    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenCadFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: default,
            CadId: id
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Act
        await handler.Handle(ie);

        // Assert
        writes.Received(1).Remove(cad);
        await uow.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldCallStorage_WhenCadFound()
    {
        // Arrange
        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: default,
            CadId: id
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Act
        await handler.Handle(ie);

        // Assert
        await storage.Received(1).DeleteFileAsync(cad.Key, ct);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCadNotFound()
    {
        // Arrange
        reads.SingleByIdAsync(id, true, ct).Returns(null as Cad);

        ProductDeletedIntegrationEvent ie = new(
            Id: default,
            ImageId: default,
            CadId: id
        );
        ProductDeletedIntegrationEventHandler handler = new(reads, writes, uow, storage);

        // Assert
        await Assert.ThrowsAsync<CadNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(ie);
        });
    }
}
