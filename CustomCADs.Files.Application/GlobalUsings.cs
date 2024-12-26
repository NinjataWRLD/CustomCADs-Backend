global using CustomCADs.Files.Domain.Cads;
global using CustomCADs.Files.Domain.Images;
global using CustomCADs.Shared.Core;
global using CadDto = (
    string Key,
    string ContentType,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto CamCoordinates,
    CustomCADs.Shared.Core.Common.Dtos.CoordinatesDto PanCoordinates
);
