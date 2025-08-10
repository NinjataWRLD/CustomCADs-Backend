using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Files.Application.Contracts;

public interface IStorageService
{
	Task<string> GetPresignedGetUrlAsync(string key, string? contentType, DateTime? expiresAt = null);
	Task<UploadFileResponse> GetPresignedPostUrlAsync(string folderPath, string name, UploadFileRequest file, DateTime? expiresAt = null);
	Task<string> GetPresignedPutUrlAsync(string key, UploadFileRequest file, DateTime? expiresAt = null);
	Task DeleteFileAsync(string key, CancellationToken ct = default);
}
