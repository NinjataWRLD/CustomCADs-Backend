using CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Delete;
using CustomCADs.Idempotency.Domain.Repositories;
using CustomCADs.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Commands.Internal.Delete;

using static IdempotencyKeysData;

public class DeleteIdempotencyKeyHandlerUnitTests : BaseIdempotencyKeyUnitTests
{
	private readonly DeleteIdempotencyKeyHandler handler;
	private readonly Mock<IIdempotencyKeyReads> reads = new();
	private readonly Mock<IWrites<IdempotencyKey>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public DeleteIdempotencyKeyHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(
			ValidId,
			ValidRequestHash,
			true,
			ct
		)).ReturnsAsync(CreateIdempotencyKey());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteIdempotencyKeyCommand command = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(
			ValidId,
			ValidRequestHash,
			true,
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteIdempotencyKeyCommand command = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(It.Is<IdempotencyKey>(x => x.Id == ValidId)), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenIdempotencyKeyNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, true, ct)).ReturnsAsync(null as IdempotencyKey);

		DeleteIdempotencyKeyCommand command = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<IdempotencyKey>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
