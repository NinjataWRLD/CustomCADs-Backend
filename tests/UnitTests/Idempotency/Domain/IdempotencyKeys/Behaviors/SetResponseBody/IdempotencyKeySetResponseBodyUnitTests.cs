namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Behaviors.SetResponseBody;

using CustomCADs.Shared.Domain.Exceptions;
using static IdempotencyKeysData;

public class IdempotencyKeySetResponseBodyUnitTests : BaseIdempotencyKeyUnitTests
{
	[Fact]
	public void SetResponseBody_ShouldNotThrow()
	{
		CreateIdempotencyKey().SetResponseBody(ValidResponseBody);
	}

	[Fact]
	public void SetResponseBody_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey();

		idempotencyKey.SetResponseBody(ValidResponseBody);

		Assert.Equal(ValidResponseBody, idempotencyKey.ResponseBody);
	}

	[Fact]
	public void SetResponseBody_ShouldThrow_WhenInvalidResponseBody()
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(
			() => CreateIdempotencyKey().SetResponseBody(InvalidResponseBody)
		);
	}
}
