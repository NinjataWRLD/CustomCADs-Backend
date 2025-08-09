using Amazon.S3;
using Amazon.S3.Model;
using CustomCADs.Files.Application.Contracts;
using CustomCADs.Shared.Core.Common.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Files.Infrastructure;

public sealed class AmazonStorageService(IAmazonS3 s3Client, IOptions<StorageSettings> settings) : IStorageService
{
	private static DateTime GetDefaultExpiresAt() => DateTime.UtcNow.AddMinutes(2);

	public async Task<string> GetPresignedGetUrlAsync(string key, string? contentType, DateTime? expiresAt = null)
	{
		try
		{
			GetPreSignedUrlRequest req = new()
			{
				BucketName = settings.Value.BucketName,
				Key = key,
				Verb = HttpVerb.GET,
				Expires = expiresAt ?? GetDefaultExpiresAt(),
				ContentType = contentType,
			};
			return await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
		}
		catch (Exception)
		{
			throw new($"Retrieving file: {key} went wrong.");
		}
	}

	public async Task<UploadFileResponse> GetPresignedPostUrlAsync(string folderPath, string name, UploadFileRequest file, DateTime? expiresAt = null)
	{
		try
		{
			string extension = file.FileName[file.FileName.LastIndexOf('.')..];
			string key = $"{folderPath}/{name}{Guid.NewGuid()}{extension}";

			GetPreSignedUrlRequest req = new()
			{
				BucketName = settings.Value.BucketName,
				Key = key,
				Verb = HttpVerb.PUT,
				Expires = expiresAt ?? GetDefaultExpiresAt(),
				ContentType = file.ContentType,
				Metadata = { ["file-name"] = file.FileName }
			};

			string url = await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
			return new(GeneratedKey: key, PresignedUrl: url);
		}
		catch (AmazonS3Exception ex)
		{
			throw new(ex.Message);
		}
		catch (Exception)
		{
			throw new($"Uploading file: {file.FileName} went wrong.");
		}
	}

	public async Task<string> GetPresignedPutUrlAsync(string key, UploadFileRequest file, DateTime? expiresAt = null)
	{
		try
		{
			GetPreSignedUrlRequest req = new()
			{
				BucketName = settings.Value.BucketName,
				Key = key,
				Verb = HttpVerb.PUT,
				Expires = expiresAt ?? GetDefaultExpiresAt(),
				ContentType = file.ContentType,
				Metadata = { ["file-name"] = file.FileName }
			};
			return await s3Client.GetPreSignedURLAsync(req).ConfigureAwait(false);
		}
		catch (AmazonS3Exception ex)
		{
			throw new(ex.Message);
		}
		catch (Exception)
		{
			throw new($"Replacing file: {file.FileName} went wrong.");
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
