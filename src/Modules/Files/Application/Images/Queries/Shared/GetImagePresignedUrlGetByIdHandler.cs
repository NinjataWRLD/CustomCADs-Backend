﻿using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.Queries.Shared;

public class GetImagePresignedUrlGetByIdHandler(IImageReads reads, IStorageService storage)
	: IQueryHandler<GetImagePresignedUrlGetByIdQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetImagePresignedUrlGetByIdQuery req, CancellationToken ct)
	{
		Image image = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(req.Id);

		string url = await storage.GetPresignedGetUrlAsync(
			key: image.Key,
			contentType: image.ContentType
		).ConfigureAwait(false);

		return new(
			PresignedUrl: url,
			ContentType: image.ContentType
		);
	}
}
