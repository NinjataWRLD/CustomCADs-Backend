using System.Reflection;

namespace CustomCADs.Catalog.Domain;

public class CatalogDomainAssemblyReference
{
    public static Assembly Assembly => typeof(CatalogDomainAssemblyReference).Assembly;
}
