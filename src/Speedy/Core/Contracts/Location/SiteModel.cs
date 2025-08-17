namespace CustomCADs.Speedy.Core.Contracts.Location;

public record SiteModel(
	long Id,
	int CountryId,
	long MainSiteId,
	string Type,
	string TypeEn,
	string Name,
	string NameEn,
	string Municipality,
	string MunicipalityEn,
	string Region,
	string RegionEn,
	string PostCode,
	int AddressNomenclature,
	double X,
	double Y,
	string ServingDays,
	int ServingOfficeid,
	int ServingHubOfficeId
);
