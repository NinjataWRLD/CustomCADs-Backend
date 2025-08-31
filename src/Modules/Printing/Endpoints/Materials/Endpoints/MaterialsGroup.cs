namespace CustomCADs.Printing.Endpoints.Materials.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class MaterialsGroup : Group
{
	public MaterialsGroup()
	{
		Configure(Paths.Materials, ep =>
		{
			ep.Roles(Admin);
			ep.Description(opt => opt.WithTags(Tags[Paths.Materials]));
		});
	}
}
