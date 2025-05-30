using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;

public sealed record SetProductFilesCommand(
	ProductId Id,
	(string? Key, string? ContentType, decimal? Volume) Cad,
	(string? Key, string? ContentType) Image,
	AccountId CreatorId
) : ICommand;
