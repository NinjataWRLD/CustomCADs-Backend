namespace CustomCADs.UnitTests.Accounts.Application.Roles.DomainEventHandlers.Deleted.Data;

using static RolesData;

public class RoleDeletedHandlerValidData : RoleDeletedHandlerData
{
    public RoleDeletedHandlerValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
        Add(ValidName4, ValidDescription4);
    }
}
