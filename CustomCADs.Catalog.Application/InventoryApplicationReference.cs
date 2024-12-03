using System.Reflection;

namespace CustomCADs.Catalog.Application;

public class InventoryApplicationReference
{
    public static Assembly Assembly => typeof(InventoryApplicationReference).Assembly;
}
