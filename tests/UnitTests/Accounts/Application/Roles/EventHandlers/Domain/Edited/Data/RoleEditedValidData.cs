﻿namespace CustomCADs.UnitTests.Accounts.Application.Roles.EventHandlers.Domain.Edited.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.EventHandlers.Domain.Edited;
using static RolesData;

public class RoleEditedValidData : RoleEditedData
{
    public RoleEditedValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
        Add(ValidName4, ValidDescription4);
    }
}
