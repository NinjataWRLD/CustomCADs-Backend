namespace CustomCADs.Idempotency.Domain.IdempotencyKeys;

using static IdempotencyKeyConstants;

public static class Validations
{
	public static IdempotencyKey ValidateIdempotencyKey(this IdempotencyKey idempotencyKey)
	{
		string property = "Id";
		IdempotencyKeyId id = idempotencyKey.Id;

		if (id.Value == Guid.Empty)
		{
			throw CustomValidationException<IdempotencyKey>.NotNull(property);
		}

		return idempotencyKey;
	}

	public static IdempotencyKey ValidateRequestHash(this IdempotencyKey idempotencyKey)
	{
		string property = "RequestHash";
		string requestHash = idempotencyKey.RequestHash;

		if (string.IsNullOrWhiteSpace(requestHash))
		{
			throw CustomValidationException<IdempotencyKey>.NotNull(property);
		}

		return idempotencyKey;
	}

	public static IdempotencyKey ValidateResponseBody(this IdempotencyKey idempotencyKey)
	{
		string property = "ResponseBody";
		string responseBody = idempotencyKey.ResponseBody;

		if (responseBody is null) // !! string.Empty is valid !!
		{
			throw CustomValidationException<IdempotencyKey>.NotNull(property);
		}

		return idempotencyKey;
	}

	public static IdempotencyKey ValidateStatusCode(this IdempotencyKey idempotencyKey)
	{
		string property = "StatusCode";
		int statusCode = idempotencyKey.StatusCode;

		int min = MinStatusCode, max = MaxStatusCode;
		if (statusCode < min || statusCode > max)
		{
			throw CustomValidationException<IdempotencyKey>.Range(property, min, max);
		}

		return idempotencyKey;
	}
}
