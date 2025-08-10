using CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;
using CustomCADs.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

using static IdempotencyKeysData;

public class GetIdempotencyKeyHandlerUnitTests : BaseIdempotencyKeyUnitTests
{
	private readonly GetIdempotencyKeyHandler handler;
	private readonly Mock<IIdempotencyKeyReads> reads = new();

	private static readonly IdempotencyKey idempotencyKey = CreateIdempotencyKey();

	public GetIdempotencyKeyHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, false, ct))
			.ReturnsAsync(idempotencyKey);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetIdempotencyKeyQuery query = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(
			ValidId,
			ValidRequestHash,
			false,
			ct
		), Times.Once());
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task Handle_ShouldReturnResult(bool isIdempotencyKeyCompleted)
	{
		// Arrange
		if (isIdempotencyKeyCompleted)
		{
			idempotencyKey.SetResponseBody(ValidResponseBody);
			idempotencyKey.SetStatusCode(MaxValidStatusCode);
		}

		GetIdempotencyKeyQuery query = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Act
		GetIdempotencyKeyDto? result = await handler.Handle(query, ct);

		// Assert
		if (isIdempotencyKeyCompleted)
		{
			Assert.Multiple(
				() => Assert.Equal(idempotencyKey.ResponseBody, result!.ResponseBody),
				() => Assert.Equal(idempotencyKey.StatusCode, result!.StatusCode)
			);
		}
		else
		{
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenIdempotencyKeyNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, false, ct)).ReturnsAsync(null as IdempotencyKey);
		GetIdempotencyKeyQuery query = new(
			IdempotencyKey: ValidId.Value,
			RequestHash: ValidRequestHash
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<IdempotencyKey>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
