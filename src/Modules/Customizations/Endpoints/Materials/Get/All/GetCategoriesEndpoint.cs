using CustomCADs.Customizations.Application.Materials.Queries.GetAll;

namespace CustomCADs.Customizations.Endpoints.Materials.Get.All;

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
