using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public class CatalogPresentationAssemblyReference
{
    public static Assembly Assembly => typeof(CatalogPresentationAssemblyReference).Assembly;
}