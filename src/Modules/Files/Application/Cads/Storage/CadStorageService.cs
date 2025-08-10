using CustomCADs.Files.Application.Contracts;
using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Files.Application.Cads.Storage;

public class CadStorageService(IStorageService service) : ICadStorageService
{
	private const string FolderPath = "cads";

	public async Task<string> GetPresignedGetUrlAsync(string key, string contentType)
		=> await service.GetPresignedGetUrlAsync(
			key: key,
			contentType: contentType,
			expiresAt: DateTime.UtcNow.AddMinutes(1)
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
