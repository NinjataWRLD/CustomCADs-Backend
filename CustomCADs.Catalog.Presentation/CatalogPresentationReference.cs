using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public class CatalogPresentationReference
{
    public static Assembly Assembly => typeof(CatalogPresentationReference).Assembly;
}