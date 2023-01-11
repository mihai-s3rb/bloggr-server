using Bloggr.Infrastructure.Models.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Interfaces
{
    public interface IAzureStorage
    {
        Task<BlobResponseDto> UploadAsync(IFormFile blob);

        Task<BlobDto> DownloadAsync(string blobFilename);

        Task<BlobResponseDto> DeleteAsync(string blobFilename);

        Task<List<BlobDto>> ListAsync();
    }
}
