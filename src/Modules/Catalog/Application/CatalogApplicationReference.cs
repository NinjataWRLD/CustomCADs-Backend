using System.Reflection;

namespace CustomCADs.Catalog.Application;

public class CatalogApplicationReference
{
	public static Assembly Assembly => typeof(CatalogApplicationReference).Assembly;
}
