namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetTimeZones.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetTimeZones;
using static AccountsData;

public class GetTimeZonesByIdsValidData : GetTimeZonesByIdsData
{
    public GetTimeZonesByIdsValidData()
    {
        Add([ValidId1, ValidId2, ValidId3, ValidId4]);
    }
}
