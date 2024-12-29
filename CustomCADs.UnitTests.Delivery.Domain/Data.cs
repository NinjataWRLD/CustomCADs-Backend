using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Domain;
public static class Data
{
    public const string ValidCountry1 = "Bulgaria";
    public const string ValidCountry2 = "Romania";
    public const string InvalidCountry = "";

    public const string ValidCity1 = "Sofia";
    public const string ValidCity2 = "Bucharest";
    public const string InvalidCity = "";

    public const string ValidReferenceId = "some-reference-id";
    public static readonly AccountId ValidBuyerId = new(Guid.NewGuid());
}
