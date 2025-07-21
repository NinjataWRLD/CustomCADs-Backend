using CustomCADs.Shared.Core.Common.Exceptions.Application;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Shared.API;

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

	public static Guid GetIdempotencyKey(this HttpRequest request, string idempotencyHeader = "Idempotency-Key")
	{
		string? header = request.Headers[idempotencyHeader];
		if (string.IsNullOrWhiteSpace(header))
		{
			string? param = request.Query.FirstOrDefault(
				x => x.Key.Equals("idempotencyKey", StringComparison.OrdinalIgnoreCase)
			).Value;

			if (string.IsNullOrWhiteSpace(param))
			{
				throw new CustomException($"'{idempotencyHeader}' header required!");
			}
			header = param;
		}
		if (!Guid.TryParse(header, out Guid idempotencyKey))
		{
			throw new CustomException($"Invalid {idempotencyHeader} format!");
		}

		return idempotencyKey;
	}
}
