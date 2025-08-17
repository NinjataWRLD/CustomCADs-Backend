namespace CustomCADs.Speedy.Core.Contracts.Location;

public record CountryModel(
	int Id,
	string Name,
	string NameEn,
	string IsoAlpha2,
	string IsoAlpha3,
	string[] PostCodeFormats,
	bool RequireState,
	int AddressType,
	string CurrencyCode,
	int DefaultOfficeId,
	(string Name, string NameEn)[] StreetTypes,
	(string Name, string NameEn)[] ComplexTypes,
	int SiteNomen
);
