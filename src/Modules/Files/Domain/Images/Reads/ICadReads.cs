using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Files.Domain.Images.Reads;

public interface IImageReads
{
    Task<Result<Image>> AllAsync(ImageQuery query, bool track = true, CancellationToken ct = default);
    Task<Image?> SingleByIdAsync(ImageId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ImageId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
