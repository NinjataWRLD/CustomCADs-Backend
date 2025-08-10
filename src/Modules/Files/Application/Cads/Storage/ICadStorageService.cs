using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Files.Application.Cads.Storage;

public interface ICadStorageService
{
	Task<string> GetPresignedGetUrlAsync(string key, string contentType);
	Task<UploadFileResponse> GetPresignedPostUrlAsync(string name, UploadFileRequest file);
	Task<string> GetPresignedPutUrlAsync(string key, UploadFileRequest file);
	Task DeleteFileAsync(string key, CancellationToken ct = default);
}
