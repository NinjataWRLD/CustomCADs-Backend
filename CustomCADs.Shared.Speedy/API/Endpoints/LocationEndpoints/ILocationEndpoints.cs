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
    Task<GetCountryResponse> GetCountry(int id, GetCountryRequest request, CancellationToken ct = default);

    [Post("country")]
    Task<FindCountryResponse> FindCountry(FindCountryRequest request, CancellationToken ct = default);

    [Post("country/csv")]
    Task<HttpResponseMessage> GetAllCountries(GetAllCountriesRequest request, CancellationToken ct = default);


    // State

    [Post("state/{id}")]
    Task<GetStateResponse> GetState(string id, GetStateRequest request, CancellationToken ct = default);

    [Post("state")]
    Task<FindStateResponse> FindState(FindStateRequest request, CancellationToken ct = default);

    [Post("state/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllStates(int countryId, GetAllStatesRequest request, CancellationToken ct = default);


    // Site

    [Post("site/{id}")]
    Task<GetSiteResponse> GetSite(long id, GetSiteRequest request, CancellationToken ct = default);

    [Post("site")]
    Task<FindSiteResponse> FindSite(FindSiteRequest request, CancellationToken ct = default);

    [Post("site/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllSites(int countryId, GetAllSitesRequest request, CancellationToken ct = default);


    // Site

    [Post("street/{id}")]
    Task<GetStreetResponse> GetStreet(long id, GetStreetRequest request, CancellationToken ct = default);

    [Post("street")]
    Task<FindStreetResponse> FindStreet(FindStreetRequest request, CancellationToken ct = default);

    [Post("street/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllStreets(int countryId, GetAllComplexRequest request, CancellationToken ct = default);


    // Complex

    [Post("complex/{id}")]
    Task<GetComplexResponse> GetComplex(long id, GetComplexRequest request, CancellationToken ct = default);

    [Post("complex")]
    Task<FindComplexResponse> FindComplex(FindComplexRequest request, CancellationToken ct = default);

    [Post("complex/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllComplexes(int countryId, GetAllComplexesRequest request, CancellationToken ct = default);


    // Block

    [Post("block")]
    Task<FindBlockResponse> FindBlock(FindBlockRequest request, CancellationToken ct = default);

    [Post("block/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllBlocks(int countryId, GetAllBlocksRequest request, CancellationToken ct = default);


    // Point of Interest

    [Post("poi/{id}")]
    Task<GetPoiResponse> GetPoi(long id, GetPoiRequest request, CancellationToken ct = default);

    [Post("poi")]
    Task<FindPoiResponse> FindPoi(FindPoiRequest request, CancellationToken ct = default);

    [Post("poi/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllPois(int countryId, GetAllPoisRequest request, CancellationToken ct = default);


    // Post Code

    [Post("postcode/csv/{countryId}")]
    Task<HttpResponseMessage> GetAllPostcodes(int countryId, GetAllPostcodesRequest request, CancellationToken ct = default);


    // Office

    [Post("office/{id}")]
    Task<GetOfficeResponse> GetOffice(int id, GetOfficeRequest request, CancellationToken ct = default);

    [Post("office")]
    Task<FindOfficeResponse> FindOffice(FindOfficeRequest request, CancellationToken ct = default);

    [Post("office/nearest-offices")]
    Task<FindNearestOfficesResponse> FindNearestOffices(FindNearestOfficesRequest request, CancellationToken ct = default);
}
