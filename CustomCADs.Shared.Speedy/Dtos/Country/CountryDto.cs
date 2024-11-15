namespace CustomCADs.Shared.Speedy.Dtos.Country;

public record CountryDto(
    int Id,
    string Name,
    string NameEn,
    string IsoAlpha2,
    string IsoAlpha3,
    string[] PostCodeFormats,
    string RequireState,
    int AddressType,
    string CurrencyCode,
    int DefaultOfficeId,
    AddressNomenclatureTypeDto[] StreetTypes,
    AddressNomenclatureTypeDto[] ComplexTypes,
    int SiteNomen
);
