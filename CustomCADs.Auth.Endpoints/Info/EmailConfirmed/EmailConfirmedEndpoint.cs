namespace CustomCADs.Auth.Endpoints.Info.EmailConfirmed;
public class EmailConfirmedEndpoint(IUserService service) : Endpoint<EmailConfirmedRequest>
{
    public override void Configure()
    {
        Get("emailConfirmed/{username}");
        Group<InfoGroup>();
    }

    public override async Task HandleAsync(EmailConfirmedRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);
        bool isEmailConfirmed = user?.EmailConfirmed ?? false;

        await SendOkAsync(isEmailConfirmed).ConfigureAwait(false);
    }
}
