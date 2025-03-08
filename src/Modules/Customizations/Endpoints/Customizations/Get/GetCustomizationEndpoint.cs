using CustomCADs.Customizations.Application.Customizations.Queries.GetById;

namespace CustomCADs.Customizations.Endpoints.Customizations.Get;

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
