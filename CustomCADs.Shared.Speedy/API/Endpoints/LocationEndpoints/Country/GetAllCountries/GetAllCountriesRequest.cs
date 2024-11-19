﻿namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Country.GetAllCountries;

public record GetAllCountriesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);