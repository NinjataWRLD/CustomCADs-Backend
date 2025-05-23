﻿using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Data;

using static Constants.Roles;
using static RoleConstants;

public static class RolesData
{
    public static readonly RoleId ValidId1 = RoleId.New();
    public static readonly RoleId ValidId2 = RoleId.New();
    public static readonly RoleId ValidId3 = RoleId.New();
    public static readonly RoleId ValidId4 = RoleId.New();

    public const string ValidName1 = Customer;
    public const string ValidName2 = Contributor;
    public const string ValidName3 = Designer;
    public const string ValidName4 = Admin;
    public const string InvalidName1 = "";
    public static readonly string InvalidName2 = new('a', NameMinLength - 1);
    public static readonly string InvalidName3 = new('a', NameMaxLength + 1);

    public const string ValidDescription1 = CustomerDescription;
    public const string ValidDescription2 = ContributorDescription;
    public const string ValidDescription3 = DesignerDescription;
    public const string ValidDescription4 = AdminDescription;
    public const string InvalidDescription1 = "";
    public static readonly string InvalidDescription2 = new('a', DescriptionMinLength - 1);
    public static readonly string InvalidDescription3 = new('a', DescriptionMaxLength + 1);
}
