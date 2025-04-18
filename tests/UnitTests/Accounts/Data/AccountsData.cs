﻿using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Data;

using static AccountConstants;
using static Constants.Users;

public static class AccountsData
{
    public static readonly AccountId ValidId1 = AccountId.New(CustomerAccountId);
    public static readonly AccountId ValidId2 = AccountId.New(ContributorAccountId);
    public static readonly AccountId ValidId3 = AccountId.New(DesignerAccountId);
    public static readonly AccountId ValidId4 = AccountId.New(AdminAccountId);

    public const string ValidUsername1 = CustomerUsername;
    public const string ValidUsername2 = ContributorUsername;
    public const string ValidUsername3 = DesignerUsername;
    public const string ValidUsername4 = AdminUsername;
    public const string InvalidUsername1 = "";
    public static readonly string InvalidUsername2 = new('a', NameMinLength - 1);
    public static readonly string InvalidUsername3 = new('a', NameMaxLength + 1);

    public const string ValidEmail1 = CustomerEmail;
    public const string ValidEmail2 = ContributorEmail;
    public const string ValidEmail3 = DesignerEmail;
    public const string ValidEmail4 = AdminEmail;
    public const string InvalidEmail1 = "";
    public const string InvalidEmail2 = "a@a";
    public const string InvalidEmail3 = "a@a.a";
    public const string InvalidEmail4 = " a@a.co";
    public const string InvalidEmail5 = "a@a.co ";

    public const string ValidPassword = "password123";

    public const string? ValidFirstName1 = "John";
    public const string? ValidFirstName2 = null;
    public static readonly string InvalidFirstName1 = new('a', NameMinLength - 1);
    public static readonly string InvalidFirstName2 = new('a', NameMaxLength + 1);

    public const string? ValidLastName1 = "Doe";
    public const string? ValidLastName2 = null;
    public static readonly string InvalidLastName1 = new('a', NameMinLength - 1);
    public static readonly string InvalidLastName2 = new('a', NameMaxLength + 1);
}
