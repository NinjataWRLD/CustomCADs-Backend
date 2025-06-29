﻿using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Application.Images;

using static ImagesData;

public class ImagesBaseUnitTests
{
	protected static readonly ImageId id = ImageId.New();
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Image CreateImage(string key = ValidKey, string contentType = ValidContentType)
		=> Image.Create(key, contentType);

	protected static Image CreateImageWithId(ImageId? id = null, string key = ValidKey, string contentType = ValidContentType)
		=> Image.CreateWithId(id ?? ImagesBaseUnitTests.id, key, contentType);
}
