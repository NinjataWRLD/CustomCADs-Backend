using Amazon.S3;
using Amazon.S3.Model;
using CustomCADs.Shared.Core.Storage;
using CustomCADs.Shared.Core.Storage.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Shared.Infrastructure.Storage;

public class AmazonS3Service(IAmazonS3 s3Client, IOptions<StorageSettings> settings) : IStorageService
{
    public async Task<string> GetPresignedGetUrlAsync(string path)
    {
        try
        {
            GetPreSignedUrlRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = path,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(2),
            };
            string url = await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
            return url;
        }
        catch (Exception)
        {
            throw new($"Retrieving file: {path} went wrong.");
        }
    }
    
    public async Task<string> GetPresignedPostUrlAsync(string path, string contentType, string fileName)
    {
        try
        {
            GetPreSignedUrlRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = path,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(12),
                ContentType = contentType,
                Metadata = { ["file-name"] = fileName }
            };
            string url = await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
            return url;
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Retrieving file: {path} went wrong.");
        }
    }

    public async Task<DownloadFileDto> DownloadFileAsync(string path, CancellationToken ct = default)
    {
        try
        {
            GetObjectRequest req = new()
            {
                Key = path,
                BucketName = settings.Value.BucketName,
            };
            GetObjectResponse res = await s3Client.GetObjectAsync(req, ct);

            DownloadFileDto response = new(
                res.ResponseStream,
                res.Headers.ContentType,
                res.Metadata["file-name"]
            );
            return response;
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Retrieving file: {path} went wrong.");
        }
    }

    public async Task<string> UploadFileAsync(string folderPath, Stream stream, string contentType, string fileName, CancellationToken ct = default)
    {
        string path = $"{folderPath}/{Guid.NewGuid()}";
        try
        {
            PutObjectRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = path,
                InputStream = stream,
                ContentType = contentType,
                Metadata = { ["file-name"] = fileName },
            };
            await s3Client.PutObjectAsync(req, ct).ConfigureAwait(false);

            return path;
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Uploading file: {fileName} went wrong.");
        }
    }

    public async Task DeleteFileAsync(string path, CancellationToken ct = default)
    {
        try
        {
            DeleteObjectRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = path,
            };
            await s3Client.DeleteObjectAsync(req, ct).ConfigureAwait(false);
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Deleting file: {path} went wrong.");
        }
    }
}
