namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted.Data;

using static RolesData;

public class RoleDeletedValidData : RoleDeletedData
{
    public RoleDeletedValidData()
    {
        Add(Role.Create(ValidName1, ValidDescription1));
        Add(Role.Create(ValidName2, ValidDescription2));
        Add(Role.Create(ValidName3, ValidDescription3));
        Add(Role.Create(ValidName4, ValidDescription4));
    }
}
