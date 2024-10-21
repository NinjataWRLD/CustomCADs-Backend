using System.Reflection;

namespace CustomCADs.Catalog.Domain;

public class CatalogDomainReference
{
    public static Assembly Assembly => typeof(CatalogDomainReference).Assembly;
}
