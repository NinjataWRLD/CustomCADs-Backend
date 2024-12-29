namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZones.Data;

using static AccountsData;

public class GetTimeZonesByIdsHandlerValidData : GetTimeZonesByIdsHandlerData
{
    public GetTimeZonesByIdsHandlerValidData()
    {
        Add([ValidId1, ValidId2, ValidId3, ValidId4]);
    }
}
