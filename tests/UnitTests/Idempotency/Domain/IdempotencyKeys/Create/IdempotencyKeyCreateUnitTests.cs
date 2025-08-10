using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Create;

using static IdempotencyKeysData;

public class IdempotencyKeyCreateUnitTests : BaseIdempotencyKeyUnitTests
{
	[Fact]
	public void Create_ShouldNotThrow()
	{
		CreateIdempotencyKey();
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey(
			id: ValidId,
			hash: ValidRequestHash
		);
		TimeSpan timeSinceCreation = DateTimeOffset.UtcNow - idempotencyKey.CreatedAt;

		Assert.Multiple(
			() => Assert.Equal(ValidId, idempotencyKey.Id),
			() => Assert.Equal(ValidRequestHash, idempotencyKey.RequestHash),
			() => Assert.True(timeSinceCreation < TimeSpan.FromSeconds(1))
		);
	}

	[Theory]
	[ClassData(typeof(Data.IdempotencyKeyCreateInvalidData))]
	public void Create_ShouldThrow_WhenIdempotencyKeyInvalid(IdempotencyKeyId id, string requestHash)
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(
			() => CreateIdempotencyKey(id, requestHash)
		);
	}
}
