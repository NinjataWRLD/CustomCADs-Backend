using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Files.Application.Images.Storage;

public interface IImageStorageService
{
	Task<string> GetPresignedGetUrlAsync(string key);
	Task<UploadFileResponse> GetPresignedPostUrlAsync(string name, UploadFileRequest file);
	Task<string> GetPresignedPutUrlAsync(string key, UploadFileRequest file);
	Task DeleteFileAsync(string key, CancellationToken ct = default);
}
