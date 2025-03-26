namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetTimeZone.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetTimeZone;
using static AccountsData;

public class GetTimeZoneByIdValidData : GetTimeZoneByIdData
{
    public GetTimeZoneByIdValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
        Add(ValidId4);
    }
}
