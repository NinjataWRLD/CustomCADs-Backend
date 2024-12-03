using CustomCADs.Categories.Application.Categories.Commands;
using CustomCADs.Categories.Application.Categories.Commands.Edit;

namespace CustomCADs.Categories.Endpoints.Categories.Put;

public sealed class PutCategoryEndpoint(IRequestSender sender)
    : Endpoint<PutCategoryRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("4. Edit")
            .WithDescription("Edit a Category by specifying its Id and a new Name")
        );
    }

    public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
    {
        EditCategoryCommand command = new(
            Id: new CategoryId(req.Id),
            Dto: new CategoryWriteDto(req.Name)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
