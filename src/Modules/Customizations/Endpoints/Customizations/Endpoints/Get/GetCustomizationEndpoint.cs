using CustomCADs.Customizations.Application.Customizations.Dtos;
using CustomCADs.Customizations.Application.Customizations.Queries.Internal.GetById;
using CustomCADs.Customizations.Endpoints.Customizations.Endpoints;

namespace CustomCADs.Customizations.Endpoints.Customizations.Endpoints.Get;

public class GetCustomizationEndpoint(IRequestSender sender)
    : Endpoint<GetCustomizationRequest, CustomizationResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<CustomizationsGroup>();
        Description(d => d
            .WithSummary("02. Get")
            .WithDescription("Get a Customization")
        );
    }

    public override async Task HandleAsync(GetCustomizationRequest req, CancellationToken ct)
    {
        GetCustomizationByIdQuery query = new(
            Id: CustomizationId.New(req.Id)
        );
        CustomizationDto customization = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CustomizationResponse response = customization.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
