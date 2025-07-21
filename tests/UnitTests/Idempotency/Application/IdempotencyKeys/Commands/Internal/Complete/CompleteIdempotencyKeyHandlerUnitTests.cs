using CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;
using CustomCADs.Idempotency.Domain.Repositories;
using CustomCADs.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;

using static IdempotencyKeysData;

public class CompleteIdempotencyKeyHandlerUnitTests : BaseIdempotencyKeyUnitTests
{
	private readonly CompleteIdempotencyKeyHandler handler;
	private readonly Mock<IIdempotencyKeyReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CompleteIdempotencyKeyHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

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
		CompleteIdempotencyKeyCommand command = new(
			Id: ValidId,
			RequestHash: ValidRequestHash,
			ResponseBody: ValidResponseBody,
			StatusCode: MaxValidStatusCode
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
		CompleteIdempotencyKeyCommand command = new(
			Id: ValidId,
			RequestHash: ValidRequestHash,
			ResponseBody: ValidResponseBody,
			StatusCode: MaxValidStatusCode
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenIdempotencyKeyNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, true, ct)).ReturnsAsync(null as IdempotencyKey);

		CompleteIdempotencyKeyCommand command = new(
			Id: ValidId,
			RequestHash: ValidRequestHash,
			ResponseBody: ValidResponseBody,
			StatusCode: MaxValidStatusCode
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<IdempotencyKey>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
