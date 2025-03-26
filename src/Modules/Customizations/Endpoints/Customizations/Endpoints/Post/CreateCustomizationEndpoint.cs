using CustomCADs.Customizations.Application.Customizations.Commands.Internal.Create;
using CustomCADs.Customizations.Application.Customizations.Dtos;
using CustomCADs.Customizations.Application.Customizations.Queries.Internal.GetById;
using CustomCADs.Customizations.Endpoints.Customizations.Endpoints;
using CustomCADs.Customizations.Endpoints.Customizations.Endpoints.Get;

namespace CustomCADs.Customizations.Endpoints.Customizations.Endpoints.Post;

public class CreateCustomizationEndpoint(IRequestSender sender)
    : Endpoint<CreateCustomizationRequest, CustomizationResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CustomizationsGroup>();
        Description(d => d
            .WithSummary("01. Create")
            .WithDescription("Create a Customization")
        );
    }

    public override async Task HandleAsync(CreateCustomizationRequest req, CancellationToken ct)
    {
        CreateCustomizationCommand command = new(
            Scale: req.Scale,
            Infill: req.Infill,
            Volume: req.Volume,
            Color: req.Color,
            MaterialId: MaterialId.New(req.MaterialId)
        );
        CustomizationId customizationId = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetCustomizationByIdQuery query = new(customizationId);
        CustomizationDto customization = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CustomizationResponse response = customization.ToResponse();
        await SendCreatedAtAsync<GetCustomizationEndpoint>(new { id = customizationId.Value }, response).ConfigureAwait(false);
    }
}
