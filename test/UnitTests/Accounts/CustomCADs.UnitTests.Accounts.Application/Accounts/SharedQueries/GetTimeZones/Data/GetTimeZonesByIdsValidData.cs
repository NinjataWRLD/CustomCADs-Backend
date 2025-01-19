namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZones.Data;

using static AccountsData;

public class GetTimeZonesByIdsValidData : GetTimeZonesByIdsData
{
    public GetTimeZonesByIdsValidData()
    {
        Add([ValidId1, ValidId2, ValidId3, ValidId4]);
    }
}
