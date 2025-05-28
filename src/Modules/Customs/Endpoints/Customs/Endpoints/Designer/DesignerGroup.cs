namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer;

using static Constants.Roles;

public class DesignerGroup : Group
{
	public DesignerGroup()
	{
		Configure("customs/designer", ep =>
		{
			ep.Roles(Designer);
			ep.Description(d => d.WithTags("10. Customs Management"));
		});
	}
}
