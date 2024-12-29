namespace CustomCADs.UnitTests.Accounts.Application.Roles.DomainEventHandlers.Created.Data;

using static RolesData;

public class RoleCreatedHandlerValidData : RoleCreatedHandlerData
{
    public RoleCreatedHandlerValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
        Add(ValidName4, ValidDescription4);
    }
}
