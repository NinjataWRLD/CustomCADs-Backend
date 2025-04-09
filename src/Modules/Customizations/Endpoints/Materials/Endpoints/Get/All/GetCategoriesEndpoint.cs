using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetAll;
using CustomCADs.Customizations.Endpoints.Materials.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.All;

public sealed class GetCategoriesEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<MaterialResponse[]>
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("All")
            .WithDescription("See all Materials")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<MaterialDto> categories = await sender.SendQueryAsync(
            new GetAllMaterialsQuery(),
            ct
        ).ConfigureAwait(false);

        MaterialResponse[] response = [.. categories.Select(c => c.ToResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
