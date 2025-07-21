using CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;
using CustomCADs.Idempotency.Domain.Repositories;
using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;

using static IdempotencyKeysData;

public class CreateIdempotencyKeyHandlerUnitTests : BaseIdempotencyKeyUnitTests
{
	private readonly CreateIdempotencyKeyHandler handler;
	private readonly Mock<IWrites<IdempotencyKey>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateIdempotencyKeyHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<IdempotencyKey>(x =>
				x.Id == ValidId
				&& x.RequestHash == ValidRequestHash
			),
			ct
		)).ReturnsAsync(CreateIdempotencyKey());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateIdempotencyKeyCommand command = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<IdempotencyKey>(x =>
				x.Id == ValidId
				&& x.RequestHash == ValidRequestHash
			),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateIdempotencyKeyCommand command = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Act
		IdempotencyKeyId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
