﻿using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetById;
using CustomCADs.Customizations.Endpoints.Materials.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.Single;

public sealed class GetMaterialEndpoint(IRequestSender sender)
	: Endpoint<GetMaterialRequest, MaterialResponse>
{
	public override void Configure()
	{
		Get("{id}");
		AllowAnonymous();
		Group<MaterialsGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See a Material")
		);
	}

	public override async Task HandleAsync(GetMaterialRequest req, CancellationToken ct)
	{
		MaterialDto category = await sender.SendQueryAsync(
			new GetMaterialByIdQuery(
				Id: MaterialId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);

		MaterialResponse response = category.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
