namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;

using static Constants.Roles;

public class CustomerGroup : Group
{
    public CustomerGroup()
    {
        Configure("customs/customer", ep =>
        {
            ep.Roles(Customer);
            ep.Description(d => d.WithTags("07. Customs"));
        });
    }
}
