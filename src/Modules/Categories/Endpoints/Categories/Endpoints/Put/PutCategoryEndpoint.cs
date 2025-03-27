using CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;

namespace CustomCADs.Categories.Endpoints.Categories.Endpoints.Put;

public sealed class PutCategoryEndpoint(IRequestSender sender)
    : Endpoint<PutCategoryRequest>
{
    public override void Configure()
    {
        Put("");
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("Edit")
            .WithDescription("Edit a Category")
        );
    }

    public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
    {
        EditCategoryCommand command = new(
            Id: CategoryId.New(req.Id),
            Dto: new CategoryWriteDto(req.Name, req.Description)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
