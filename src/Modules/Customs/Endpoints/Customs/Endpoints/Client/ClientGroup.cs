namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("customs/client", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("07. Customs"));
        });
    }
}
