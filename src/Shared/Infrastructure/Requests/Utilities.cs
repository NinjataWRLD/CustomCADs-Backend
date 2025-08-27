using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CustomCADs.Shared.Infrastructure.Requests;

internal static class Utilities
{
	internal static Expiration CaclulateExpiration(this AddRequestCachingAttribute[] attributes)
	{
		Expiration expiration = new(Absolute: null, Sliding: null);

		foreach (AddRequestCachingAttribute attribute in attributes)
		{
			expiration = attribute.Expiration switch
			{
				{ Absolute: not null } => expiration with { Absolute = attribute.Expiration.Absolute },
				{ Sliding: not null } => expiration with { Sliding = attribute.Expiration.Sliding },
				_ => expiration,
			};
		}

		return expiration;
	}

	internal static string Hash<TResponse>(this IQuery<TResponse> request, string group)
		=> HashRequest(request, group);

	internal static string Hash<TResponse>(this ICommand<TResponse> request, string group)
		=> HashRequest(request, group);

	internal static string Hash(this ICommand request, string group)
		=> HashRequest(request, group);

	private static string HashRequest(object request, string group)
		=> $"{group}/{request.GetType().FullName}:{Convert.ToHexString(
			SHA256.HashData(
				source: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request))
			)
		)}";
}
