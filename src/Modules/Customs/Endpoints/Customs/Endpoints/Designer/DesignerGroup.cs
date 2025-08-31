namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer;

using static Constants.Roles;
using static EndpointsConstants;

public class DesignerGroup : Group
{
	public DesignerGroup()
	{
		Configure(Paths.CustomsDesigner, ep =>
		{
			ep.Roles(Designer);
			ep.Description(d => d.WithTags(Tags[Paths.CustomsDesigner]));
		});
	}
}
