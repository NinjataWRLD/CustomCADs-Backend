﻿namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetByName.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetByName;
using static RolesData;

public class GetRoleByNameValidData : GetRoleByNameData
{
    public GetRoleByNameValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
        Add(ValidName3);
        Add(ValidName4);
    }
}
