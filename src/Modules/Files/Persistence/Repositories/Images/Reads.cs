using CustomCADs.Files.Domain.Images;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Files.Persistence.Repositories.Images;

public sealed class Reads(FilesContext context) : IImageReads
{
	public async Task<Image?> SingleByIdAsync(ImageId id, bool track = true, CancellationToken ct = default)
		=> await context.Images
			.WithTracking(track)
			.FirstOrDefaultAsync(c => c.Id == id, ct)
			.ConfigureAwait(false);
}
