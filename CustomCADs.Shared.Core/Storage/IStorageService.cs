using CustomCADs.Shared.Core.Storage.Dtos;

namespace CustomCADs.Shared.Core.Storage;

public interface IStorageService
{
    Task<string> GetPresignedGetUrlAsync(string path);
    Task<string> GetPresignedPostUrlAsync(string path, string contentType, string fileName);
    Task<DownloadFileDto> DownloadFileAsync(string path, CancellationToken ct = default);
    Task<string> UploadFileAsync(string folderPath, Stream stream, string contentType, string fileName, CancellationToken ct = default);
    Task DeleteFileAsync(string path, CancellationToken ct = default);
}
