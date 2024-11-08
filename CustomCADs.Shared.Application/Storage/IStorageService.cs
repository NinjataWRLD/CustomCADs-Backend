using CustomCADs.Shared.Application.Storage.Dtos;

namespace CustomCADs.Shared.Application.Storage;

public interface IStorageService
{
    Task<string> GetPresignedGetUrlAsync(string path);
    Task<string> GetPresignedPostUrlAsync(string path, string contentType, string fileName);
    Task<DownloadFileDto> DownloadFileAsync(string path, CancellationToken ct = default);
    Task<string> UploadFileAsync(string folderPath, Stream stream, Guid id, string name, string contentType, string fileName, CancellationToken ct = default);
    Task DeleteFileAsync(string path, CancellationToken ct = default);
}
