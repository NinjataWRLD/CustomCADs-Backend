namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;

using static Constants.Roles;
using static EndpointsConstants;

public class CustomerGroup : Group
{
	public CustomerGroup()
	{
		Configure(Paths.CustomsCustomer, ep =>
		{
			ep.Roles(Customer);
			ep.Description(d => d.WithTags(Tags[Paths.CustomsCustomer]));
		});
	}
}
