using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Data;

using static Constants.Users;
using static ShipmentConstants;

public static class ShipmentsData
{
	public const string ValidCountry = "Bulgaria";
	public const string InvalidCountry = "";

	public const string ValidCity = "Sofia";
	public const string InvalidCity = "";

	public const string ValidStreet = "Flora";
	public const string InvalidStreet = "";

	public const string? ValidPhone = "+1234567890";
	public const string? InvalidPhone = "abcdefghijklmnopqrstuvwxyz";

	public const string? ValidEmail = "john.doe@example.com";
	public const string? InvalidEmail = "";

	public const string ValidService = "Standard";
	public const string InvalidService = "";

	public const int MinValidCount = MinCount + 1;
	public const int MaxValidCount = MaxCount - 1;
	public const int MinInvalidCount = MinCount - 1;
	public const int MaxInvalidCount = MaxCount + 1;

	public const double MinValidWeight = MinWeight + 1;
	public const double MaxValidWeight = MaxWeight - 1;
	public const double MinInvalidWeight = MinWeight - 1;
	public const double MaxInvalidWeight = MaxWeight + 1;

	public const string ValidRecipient = "John Doe";
	public const string InvalidRecipient = "";

	public const string ValidReferenceId = "some-reference-id";

	public static readonly ShipmentId ValidId = ShipmentId.New();
	public static readonly AccountId ValidBuyerId = AccountId.New(CustomerAccountId);
}
