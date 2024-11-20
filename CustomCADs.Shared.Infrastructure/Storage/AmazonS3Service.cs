﻿using Amazon.S3;
using Amazon.S3.Model;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.Application.Storage.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Shared.Infrastructure.Storage;

public class AmazonS3Service(IAmazonS3 s3Client, IOptions<StorageSettings> settings) : IStorageService
{
    public async Task<string> GetPresignedGetUrlAsync(string key)
    {
        try
        {
            GetPreSignedUrlRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = key,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(2),
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

    public async Task<DownloadFileDto> DownloadFileAsync(string key, CancellationToken ct = default)
    {
        try
        {
            GetObjectRequest req = new()
            {
                Key = key,
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
            throw new($"Retrieving file: {key} went wrong.");
        }
    }

    public async Task<string> UploadFileAsync(string folderPath, Stream stream, Guid id, string name, string contentType, string fileName, CancellationToken ct = default)
    {
        string extension = fileName.Remove(0, fileName.LastIndexOf('.'));
        string key = $"{folderPath}/{name}{id}{extension}";

        try
        {
            PutObjectRequest req = new()
            {
                BucketName = settings.Value.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                Metadata = { ["file-name"] = fileName },
            };
            await s3Client.PutObjectAsync(req, ct).ConfigureAwait(false);

            return key;
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
