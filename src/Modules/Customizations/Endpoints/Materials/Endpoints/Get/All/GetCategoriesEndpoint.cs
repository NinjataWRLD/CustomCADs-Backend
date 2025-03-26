using CustomCADs.Customizations.Application.Materials.Dtos;
using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetAll;
using CustomCADs.Customizations.Endpoints.Materials.Dtos;
using CustomCADs.Customizations.Endpoints.Materials.Endpoints;

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
            .WithSummary("1. All")
            .WithDescription("See all Materials")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllMaterialsQuery query = new();
        IEnumerable<MaterialDto> categories = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        MaterialResponse[] response = [.. categories.Select(c => c.ToResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
