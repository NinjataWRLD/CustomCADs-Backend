using Amazon.S3;
using Amazon.S3.Model;
using CustomCADs.Shared.Application.Storage;
using Microsoft.Extensions.Options;

namespace CustomCADs.Shared.Infrastructure.Storage;

public class AmazonS3Service(IAmazonS3 s3Client, IOptions<StorageSettings> settings) : IStorageService
{
    public async Task<string> GetPresignedGetUrlAsync(string key, string contentType)
    {
        try
        {
            GetPreSignedUrlRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = key,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(2),
                ContentType = contentType,
            };
            return await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw new($"Retrieving file: {key} went wrong.");
        }
    }

    public async Task<(string Key, string Url)> GetPresignedPostUrlAsync(string folderPath, string name, string contentType, string fileName)
    {
        try
        {
            string extension = fileName.Remove(0, fileName.LastIndexOf('.'));
            string key = $"{folderPath}/{name}{Guid.NewGuid()}{extension}";

            GetPreSignedUrlRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(2),
                ContentType = contentType,
                Metadata = { ["file-name"] = fileName }
            };

            string url = await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
            return (Key: key, Url: url);
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Retrieving file: {fileName} went wrong.");
        }
    }
    
    public async Task<string> GetPresignedPutUrlAsync(string key, string contentType, string fileName)
    {
        try
        {
            GetPreSignedUrlRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(2),
                ContentType = contentType,
                Metadata = { ["file-name"] = fileName }
            };
            return await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Retrieving file: {fileName} went wrong.");
        }
    }

    public async Task DeleteFileAsync(string key, CancellationToken ct = default)
    {
        try
        {
            DeleteObjectRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = key,
            };
            await s3Client.DeleteObjectAsync(req, ct).ConfigureAwait(false);
        }
        catch (AmazonS3Exception ex)
        {
            throw new(ex.Message);
        }
        catch (Exception)
        {
            throw new($"Deleting file: {key} went wrong.");
        }
    }
}
