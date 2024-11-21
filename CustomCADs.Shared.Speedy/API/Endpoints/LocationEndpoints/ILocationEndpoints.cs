using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;

using Block.FindBlock;
using Block.GetAllBlocks;
using Complex.FindComplex;
using Complex.GetAllComplexes;
using Complex.GetComplex;
using Country.FindCountry;
using Country.GetAllCountries;
using Country.GetCountry;
using Office.FindNearestOffice;
using Office.FindOffice;
using Office.GetOffice;
using Poi.FindPoi;
using Poi.GetAllPois;
using Poi.GetPoi;
using Postcode.GetAllPostcodes;
using Site.FindSite;
using Site.GetAllSites;
using Site.GetSite;
using State.FindState;
using State.GetAllStates;
using State.GetState;
using Street.FindStreet;
using Street.GetAllStreets;
using Street.GetStreet;

public interface ILocationEndpoints
{
    // Country

    [Post("country/{id}")]
    Task<GetCountryResponse> GetCountryAsync(int id, GetCountryRequest request, CancellationToken ct = default);

    [Post("country")]
    Task<FindCountryResponse> FindCountryAsync(FindCountryRequest request, CancellationToken ct = default);

    [Post("country/csv")]
    Task<HttpResponseMessage> GetAllCountriesAsync(GetAllCountriesRequest request, CancellationToken ct = default);


    // State

    [Post("state/{id}")]
    Task<GetStateResponse> GetStateAsync(string id, GetStateRequest request, CancellationToken ct = default);

    [Post("state")]
    Task<FindStateResponse> FindStateAsync(FindStateRequest request, CancellationToken ct = default);

    [Post("state/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllStatesAsync(int countryId, GetAllStatesRequest request, CancellationToken ct = default);


    // Site

    [Post("site/{id}")]
    Task<GetSiteResponse> GetSiteAsync(long id, GetSiteRequest request, CancellationToken ct = default);

    [Post("site")]
    Task<FindSiteResponse> FindSiteAsync(FindSiteRequest request, CancellationToken ct = default);

    [Post("site/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllSitesAsync(int countryId, GetAllSitesRequest request, CancellationToken ct = default);


    // Site

    [Post("street/{id}")]
    Task<GetStreetResponse> GetStreetAsync(long id, GetStreetRequest request, CancellationToken ct = default);

    [Post("street")]
    Task<FindStreetResponse> FindStreetAsync(FindStreetRequest request, CancellationToken ct = default);

    [Post("street/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllStreetsAsync(int countryId, GetAllStreetsRequest request, CancellationToken ct = default);


    // Complex

    [Post("complex/{id}")]
    Task<GetComplexResponse> GetComplexAsync(long id, GetComplexRequest request, CancellationToken ct = default);

    [Post("complex")]
    Task<FindComplexResponse> FindComplexAsync(FindComplexRequest request, CancellationToken ct = default);

    [Post("complex/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllComplexesAsync(int countryId, GetAllComplexesRequest request, CancellationToken ct = default);


    // Block

    [Post("block")]
    Task<FindBlockResponse> FindBlockAsync(FindBlockRequest request, CancellationToken ct = default);

    [Post("block/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllBlocksAsync(int countryId, GetAllBlocksRequest request, CancellationToken ct = default);


    // Point of Interest

    [Post("poi/{id}")]
    Task<GetPoiResponse> GetPointOfInterestAsync(long id, GetPoiRequest request, CancellationToken ct = default);

    [Post("poi")]
    Task<FindPoiResponse> FindPointOfInterestAsync(FindPoiRequest request, CancellationToken ct = default);

    [Post("poi/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllPointsOfInterestAsync(int countryId, GetAllPoisRequest request, CancellationToken ct = default);


    // Post Code

    [Post("postcode/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllPostCodesAsync(int countryId, GetAllPostcodesRequest request, CancellationToken ct = default);


    // Office

    [Post("office/{id}")]
    Task<GetOfficeResponse> GetOfficeAsync(int id, GetOfficeRequest request, CancellationToken ct = default);

    [Post("office")]
    Task<FindOfficeResponse> FindOfficeAsync(FindOfficeRequest request, CancellationToken ct = default);

    [Post("office/nearest-offices")]
    Task<FindNearestOfficesResponse> FindNearestOfficesAsync(FindNearestOfficesRequest request, CancellationToken ct = default);
}
