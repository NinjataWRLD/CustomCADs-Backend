using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Data;

using static Constants.Users;
using static ShipmentConstants;

public static class ShipmentsData
{
	public const string ValidCountry1 = "Bulgaria";
	public const string ValidCountry2 = "Romania";
	public const string InvalidCountry = "";

	public const string ValidCity1 = "Sofia";
	public const string ValidCity2 = "Bucharest";
	public const string InvalidCity = "";

	public const string ValidStreet1 = "Flora";
	public const string ValidStreet2 = "Brailles";
	public const string InvalidStreet = "";

	public const string? ValidPhone1 = "+1234567890";
	public const string? ValidPhone2 = null;
	public const string? InvalidPhone = "abcdefghijklmnopqrstuvwxyz";

	public const string? ValidEmail1 = "john.doe@example.com";
	public const string? ValidEmail2 = null;
	public const string? InvalidEmail = "";

	public const string ValidService1 = "Standard";
	public const string ValidService2 = "Express";
	public const string InvalidService = "";

	public const int ValidCount1 = MinCount + 1;
	public const int ValidCount2 = MaxCount - 1;
	public const int InvalidCount1 = MinCount - 1;
	public const int InvalidCount2 = MaxCount + 1;

	public const double ValidWeight1 = MinWeight + 1;
	public const double ValidWeight2 = MaxWeight - 1;
	public const double InvalidWeight1 = MinWeight - 1;
	public const double InvalidWeight2 = MaxWeight + 1;

	public const string ValidRecipient1 = "John Doe";
	public const string ValidRecipient2 = "Jane Doe";
	public const string InvalidRecipient = "";

	public const string ValidReferenceId = "some-reference-id";

	public static readonly AccountId ValidBuyerId = AccountId.New(CustomerAccountId);
	public static readonly AccountId ValidHeadDesignerId = AccountId.New(DesignerAccountId);
}
