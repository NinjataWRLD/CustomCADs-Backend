using CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;
using CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;
using CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;
using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.API;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Idempotency;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	private static bool ShouldSkipByDefault(this HttpRequest request)
		=> request.IsIdempotentBySpec() || HttpMethods.IsPatch(request.Method);

	public static void UseIdempotencyKeys(this IApplicationBuilder app)
		=> app.Use(async (context, next) =>
		{
			if (context.GetAttribute<EnforceIdempotencyAttribute>() is null)
			{
				bool endpointOptedOut = context.GetAttribute<SkipIdempotencyAttribute>() is not null;
				if (endpointOptedOut || context.Request.ShouldSkipByDefault())
				{
					await next().ConfigureAwait(false);
					return;
				}
			}

			IRequestSender sender = context.RequestServices.GetRequiredService<IRequestSender>();
			IProblemDetailsService problemDetails = context.RequestServices.GetRequiredService<IProblemDetailsService>();

			CancellationToken ct = context.RequestAborted;
			Guid idempotencyKey = context.Request.GetIdempotencyKey();
			string requestHash = await context.Request.GenerateRequestHashAsync(context.GetEndpoint()).ConfigureAwait(false);

			try
			{
				GetIdempotencyKeyDto? dto = await sender.SendQueryAsync(
					new GetIdempotencyKeyQuery(idempotencyKey, requestHash),
					ct
				).ConfigureAwait(false);

				if (dto is null)
				{
					await problemDetails.ConflictResponseAsync(
						context: context,
						ex: new CustomException("Too many identical requests at once!")
					).ConfigureAwait(false);
					return;
				}

				context.Response.StatusCode = dto.StatusCode;
				if (!string.IsNullOrWhiteSpace(dto.ResponseBody))
				{
					await context.Response.WriteAsync(dto.ResponseBody).ConfigureAwait(false);
				}
			}
			catch (CustomNotFoundException<IdempotencyKey>)
			{
				IdempotencyKeyId id = await sender.SendCommandAsync(
					new CreateIdempotencyKeyCommand(
						IdempotencyKey: idempotencyKey,
						RequestHash: requestHash
					),
					ct
				).ConfigureAwait(false);

				Stream originalBody = context.Response.Body;
				using MemoryStream stream = new();
				context.Response.Body = stream;

				try
				{
					await next().ConfigureAwait(false);
				}
				catch
				{
					context.Response.Body = originalBody;
					throw;
				}

				string responseBody = await context.Response.ExtractResponseBodyAsync(
					stream: stream,
					original: originalBody
				).ConfigureAwait(false);

				await sender.SendCommandAsync(
					new CompleteIdempotencyKeyCommand(
						Id: id,
						RequestHash: requestHash,
						ResponseBody: responseBody,
						StatusCode: context.Response.StatusCode
					),
					ct
				).ConfigureAwait(false);
			}
		});

	private static async Task<string> ExtractResponseBodyAsync(this HttpResponse response, MemoryStream stream, Stream original)
	{
		stream.Position = 0;
		using StreamReader reader = new(stream);
		string body = await reader.ReadToEndAsync().ConfigureAwait(false);

		stream.Position = 0;
		response.Body = original;
		await stream.CopyToAsync(original).ConfigureAwait(false);

		return body;
	}

	private static async Task<string> GenerateRequestHashAsync(this HttpRequest request, Endpoint? endpoint)
	{
		request.EnableBuffering();

		using StreamReader reader = new(
			stream: request.Body,
			encoding: Encoding.UTF8,
			detectEncodingFromByteOrderMarks: false,
			bufferSize: 1024,
			leaveOpen: true
		);
		string body = await reader.ReadToEndAsync().ConfigureAwait(false);

		request.Body.Position = 0;
		string endpointId = endpoint?.DisplayName ?? "unknown-endpoint";
		return ComputeHash($"{endpointId}|{body}");
	}

	private static string ComputeHash(string input)
		=> Convert.ToBase64String(
			inArray: SHA256.HashData(Encoding.UTF8.GetBytes(input))
		);
}
