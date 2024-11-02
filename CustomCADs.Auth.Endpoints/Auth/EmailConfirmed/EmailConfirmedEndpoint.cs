using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.EmailConfirmed;
public class EmailConfirmedEndpoint(IUserService service) : Endpoint<EmailConfirmedRequest>
{
    public override void Configure()
    {
        Get("emailConfirmed/{username}");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(EmailConfirmedRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);
        bool isEmailConfirmed = user?.EmailConfirmed ?? false;

        await SendOkAsync(isEmailConfirmed).ConfigureAwait(false);
    }
}
