using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.Abstractions.Storage;

public interface IStorageService
{
	Task<string> GetPresignedGetUrlAsync(string key, string contentType);
	Task<UploadFileResponse> GetPresignedPostUrlAsync(string folderPath, string name, UploadFileRequest file);
	Task<string> GetPresignedPutUrlAsync(string key, UploadFileRequest file);
	Task DeleteFileAsync(string key, CancellationToken ct = default);
}
