using CustomCADs.Shared.Application.Storage.Dtos;

namespace CustomCADs.Shared.Application.Storage;

public interface IStorageService
{
    Task<string> GetPresignedGetUrlAsync(string key);
    Task<(string Key, string Url)> GetPresignedPostUrlAsync(string folderPath, string name, string contentType, string fileName);
    Task<DownloadFileDto> DownloadFileAsync(string key, CancellationToken ct = default);
    Task<string> UploadFileAsync(string folderPath, Stream stream, Guid id, string name, string contentType, string fileName, CancellationToken ct = default);
    Task DeleteFileAsync(string key, CancellationToken ct = default);
}
