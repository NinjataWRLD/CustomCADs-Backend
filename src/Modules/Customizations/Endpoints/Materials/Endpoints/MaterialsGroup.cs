namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints;

using static Constants.Roles;

public class MaterialsGroup : Group
{
	public MaterialsGroup()
	{
		Configure("materials", ep =>
		{
			ep.Roles(Admin);
			ep.Description(opt => opt.WithTags("16. Materials Dashboard"));
		});
	}
}
