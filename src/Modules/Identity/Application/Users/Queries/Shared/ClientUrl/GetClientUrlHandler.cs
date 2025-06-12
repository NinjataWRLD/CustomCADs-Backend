using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.UseCases.Identity.Queries;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Users.Queries.Shared.ClientUrl;

public class GetClientUrlHandler(IOptions<ClientUrlSettings> settings)
	: IQueryHandler<GetClientUrlQuery, string>
{
	public Task<string> Handle(GetClientUrlQuery req, CancellationToken ct = default)
		=> Task.FromResult(settings.Value.Preferred);
}
