using CustomCADs.Files.Application.Cads.Events.Application;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.ApplicationEvents.Files;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Files.Application.Cads.Events.Application;

public class ProductDeletedHandlerUnitTests : CadsBaseUnitTests
{
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IStorageService> storage = new();
	private static readonly Cad cad = CreateCad();

	public ProductDeletedHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct)).ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			CadId: id1,
			ImageId: default
		);
		ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

		// Act
		await handler.Handle(ie);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenCadFound()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			CadId: id1,
			ImageId: default
		);
		ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

		// Act
		await handler.Handle(ie);

		// Assert
		writes.Verify(x => x.Remove(cad), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage_WhenCadFound()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			CadId: id1,
			ImageId: default
		);
		ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

		// Act
		await handler.Handle(ie);

		// Assert
		storage.Verify(x => x.DeleteFileAsync(cad.Key, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(null as Cad);

		ProductDeletedApplicationEvent ie = new(
			Id: default,
			CadId: id1,
			ImageId: default
		);
		ProductDeletedHandler handler = new(reads.Object, writes.Object, uow.Object, storage.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(async () =>
		{
			// Act
			await handler.Handle(ie);
		});
	}
}
