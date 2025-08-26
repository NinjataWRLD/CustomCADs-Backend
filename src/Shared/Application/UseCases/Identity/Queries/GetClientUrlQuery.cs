using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Shared.Application.UseCases.Identity.Queries;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetClientUrlQuery : IQuery<string>;
