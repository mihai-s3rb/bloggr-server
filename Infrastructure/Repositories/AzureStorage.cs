using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Models.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Repositories
{
    public class AzureStorage : IAzureStorage
    {
        private readonly string _storageConnectionString;

        private readonly string _storageContainerName;

        public AzureStorage(IConfiguration configuration)
        {
            _storageConnectionString = Environment.GetEnvironmentVariable("BlobConnectionString") ?? throw new ArgumentNullException();
            _storageContainerName = Environment.GetEnvironmentVariable("BlobContainerName") ?? throw new ArgumentNullException();
        }

        public Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            throw new NotImplementedException();
        }

        public Task<BlobDto> DownloadAsync(string blobFilename)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlobDto>> ListAsync()
        {
            var container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            List<BlobDto> files = new List<BlobDto>();

            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;

        }

        public async Task<BlobResponseDto> UploadAsync(IFormFile blob)
        {
            var response = new BlobResponseDto();

            var container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                // Get a reference to the blob just uploaded from the API in a container from configuration settings
                var randomGuid = Guid.NewGuid().ToString();
                BlobClient client = container.GetBlobClient(blob.FileName + randomGuid);

                // Open a stream for the file we want to upload
                await using (Stream? data = blob.OpenReadStream())
                {
                    // Upload the file async
                    await client.UploadAsync(data);
                }

                // Everything is OK and file got uploaded
                response.Status = $"File {blob.FileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;

            }
            // If the file already exists, we catch the exception and do not upload it
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File with name {blob.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            // If we get an unexpected error, we catch it here and return the error message
            catch (RequestFailedException ex)
            {
                // Log error to console and create a new response we can return to the requesting method
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            // Return the BlobUploadResponse object
            return response;
        }
    }
}
