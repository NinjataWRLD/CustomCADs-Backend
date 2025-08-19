namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;

/// <param name="Description">
///     Goods description.
///     <br />
///     Required: Yes. Max length: 50.
/// </param>
/// <param name="HsCode">
///     Harmoneous System (HS) Code.
///     <br />
///     Required: Yes. Max length: 50.
/// </param>
/// <param name="Quantity">
///     Quantity of goods.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ValuePerItem">
///     Price per item in goods currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WeightPerItem">
///     Weight per item.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="OriginCountryCode">
///     2 characters country code of goods origin.
///     <br />
///     Required: Yes. Max length: 2.
/// </param>
public record GoodsItemDto(
	string Description,
	string HsCode,
	int Quantity,
	double ValuePerItem,
	double WeightPerItem,
	string OriginCountryCode
);
