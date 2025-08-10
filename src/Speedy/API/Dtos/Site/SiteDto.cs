namespace CustomCADs.Speedy.API.Dtos.Site;

public record SiteDto(
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
