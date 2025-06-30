using CustomCADs.Files.Domain.Images;

namespace CustomCADs.Files.Domain.Repositories.Reads;

public interface IImageReads
{
	Task<Image?> SingleByIdAsync(ImageId id, bool track = true, CancellationToken ct = default);
}
