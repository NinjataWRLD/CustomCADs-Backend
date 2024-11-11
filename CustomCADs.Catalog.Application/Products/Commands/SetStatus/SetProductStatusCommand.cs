using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(ProductId Id, string Action) : ICommand;
