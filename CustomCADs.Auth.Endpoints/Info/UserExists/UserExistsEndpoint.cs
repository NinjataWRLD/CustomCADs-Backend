namespace CustomCADs.Auth.Endpoints.Info.UserExists;

public class UserExistsEndpoint(IUserService service)
    : Endpoint<UserExistsRequest>
{
    public override void Configure()
    {
        Get("userExists/{username}");
        Group<InfoGroup>();
        Description(d => d.WithSummary("3. Does the User Exist?"));
    }

    public override async Task HandleAsync(UserExistsRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);

        bool response = user is not null;
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
