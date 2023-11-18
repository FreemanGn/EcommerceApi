using System;
using EcommerceApi.Models;

namespace EcommerceApi.Repository.Interface
{
	public interface IAzureStorage
	{
		Task<BlobImageResponse> UploadAsync(IFormFile file);
		Task<BlobImage> DownloadAsync(string blobFileName);
		Task<BlobImageResponse> DeleteAsync(string blobFileName);
		Task<List<BlobImage>> ListAsync();
    }
}

