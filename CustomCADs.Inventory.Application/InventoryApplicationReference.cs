using System.Reflection;

namespace CustomCADs.Inventory.Application;

public class InventoryApplicationReference
{
    public static Assembly Assembly => typeof(InventoryApplicationReference).Assembly;
}
