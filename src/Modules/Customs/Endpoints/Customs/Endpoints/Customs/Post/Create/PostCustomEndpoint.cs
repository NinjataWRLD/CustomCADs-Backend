using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.Single;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Create;

public sealed class PostCustomEndpoint(IRequestSender sender)
    : Endpoint<PostCustomRequest, PostCustomResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Create")
            .WithDescription("Request a Custom")
        );
    }

    public override async Task HandleAsync(PostCustomRequest req, CancellationToken ct)
    {
        CreateCustomCommand command = new(
            Name: req.Name,
            Description: req.Description,
            ForDelivery: req.ForDelivery,
            BuyerId: User.GetAccountId()
        );
        CustomId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        ClientGetCustomByIdQuery query = new(
            Id: id,
            BuyerId: User.GetAccountId()
        );
        var custom = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostCustomResponse response = custom.ToPostResponse();
        await SendCreatedAtAsync<GetCustomEndpoint>(
            routeValues: new { Id = id.Value },
            responseBody: response
        ).ConfigureAwait(false);
    }
}
