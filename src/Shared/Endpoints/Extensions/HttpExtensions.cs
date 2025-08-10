using Microsoft.AspNetCore.Http;

namespace CustomCADs.Shared.Endpoints.Extensions;

public static class HttpExtensions
{
	public static TAttribute? GetAttribute<TAttribute>(this HttpContext context) where TAttribute : Attribute
		=> context.GetEndpoint()?.Metadata.GetMetadata<TAttribute>();

	public static bool IsIdempotentBySpec(this HttpRequest request)
		=> HttpMethods.IsGet(request.Method)
		|| HttpMethods.IsPut(request.Method)
		|| HttpMethods.IsDelete(request.Method);

	public static bool IsMutationBySpec(this HttpRequest request) =>
		HttpMethods.IsPost(request.Method)
		|| HttpMethods.IsPut(request.Method)
		|| HttpMethods.IsPatch(request.Method)
		|| HttpMethods.IsDelete(request.Method);

	public static bool TryGetIdempotencyKey(this HttpRequest request, out Guid idempotencyKey, string idempotencyHeader = "Idempotency-Key")
	{
		string? header = request.Headers[idempotencyHeader];
		if (string.IsNullOrWhiteSpace(header))
		{
			string? param = request.Query.FirstOrDefault(
				x => x.Key.Equals("idempotencyKey", StringComparison.OrdinalIgnoreCase)
			).Value;

			if (string.IsNullOrWhiteSpace(param))
			{
				idempotencyKey = Guid.Empty;
				return false;
			}
			header = param;
		}

		if (!Guid.TryParse(header, out idempotencyKey))
		{
			idempotencyKey = Guid.Empty;
			return false;
		}
		return true;
	}
}
