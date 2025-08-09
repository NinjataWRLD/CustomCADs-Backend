using CustomCADs.Files.Application.Contracts;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Files.Application.Images.Storage;

public class ImageStorageService(IStorageService service) : IImageStorageService
{
	private const string FolderPath = "images";

	public async Task<string> GetPresignedGetUrlAsync(string key)
		=> await service.GetPresignedGetUrlAsync(
			key: key,
			contentType: null,
			expiresAt: DateTime.UtcNow.AddMinutes(10)
		).ConfigureAwait(false);

	public async Task<UploadFileResponse> GetPresignedPostUrlAsync(string name, UploadFileRequest file)
		=> await service.GetPresignedPostUrlAsync(
			folderPath: FolderPath,
			name: name,
			file: file,
			expiresAt: DateTime.UtcNow.AddMinutes(2)
		).ConfigureAwait(false);

	public async Task<string> GetPresignedPutUrlAsync(string key, UploadFileRequest file)
		=> await service.GetPresignedPutUrlAsync(
			key: key,
			file: file,
			expiresAt: DateTime.UtcNow.AddMinutes(2)
		).ConfigureAwait(false);

	public async Task DeleteFileAsync(string key, CancellationToken ct = default)
		=> await service.DeleteFileAsync(key, ct).ConfigureAwait(false);
}
