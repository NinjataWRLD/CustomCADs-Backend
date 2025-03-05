using CustomCADs.Customizations.Application.Materials.Queries.GetById;

namespace CustomCADs.Customizations.Endpoints.Materials.Get.Single;

public sealed class GetMaterialEndpoint(IRequestSender sender)
    : Endpoint<GetMaterialRequest, MaterialResponse>
{
    public override void Configure()
    {
        Get("{id}");
        AllowAnonymous();
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("2. Single")
            .WithDescription("See a Material")
        );
    }

    public override async Task HandleAsync(GetMaterialRequest req, CancellationToken ct)
    {
        GetMaterialByIdQuery query = new(
            Id: MaterialId.New(req.Id)
        );
        MaterialDto category = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        MaterialResponse response = category.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
