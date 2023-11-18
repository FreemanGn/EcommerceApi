using System;
using System.Reflection.Metadata;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EcommerceApi.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace EcommerceApi.Repository.Interface
{
    public class AzureStorage : IAzureStorage
	{
        private readonly string _storageConnectionString;
        private string _storageContainerName;
        private ILogger<AzureStorage> _logger;

        public AzureStorage(IConfiguration configuration, ILogger<AzureStorage> logger)
		{
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _logger = logger;
        }

        public async Task<List<BlobImage>> ListAsync()
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            List<BlobImage> files = new List<BlobImage>();

            await foreach(BlobItem file in container.GetBlobsAsync())
            {
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobImage
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }
            return  files;
        }

        public async Task<BlobImageResponse> UploadAsync(IFormFile file)
        {
            BlobImageResponse response = new();

            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient blobClient = container.GetBlobClient(file.FileName);

                await using(Stream? data = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(data);
                }
                response.Status = $"File {file.FileName} Uploaded Successfully";
                response.Error = false;
                response.BlobImage.Uri = blobClient.Uri.AbsoluteUri;
                response.BlobImage.Name = blobClient.Name;
            }
            catch (RequestFailedException ex)
                when(ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {file.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {file.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            catch(RequestFailedException ex)
            {
                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }
            return response;
        }

        public async Task<BlobImageResponse> DeleteAsync(string fileName)
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            BlobClient file = container.GetBlobClient(fileName);

            try
            {
                await file.DeleteAsync();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // File did not exist, log to console and return new response to requesting method
                _logger.LogError($"File {fileName} was not found.");
                return new BlobImageResponse { Error = true, Status = $"File with name {fileName} not found." };
            }
            return new BlobImageResponse { Error = false, Status = $"File: {fileName} has been successfully deleted." };
        }

        public Task<BlobImage> DownloadAsync(string blobFileName)
        {
            throw new NotImplementedException();
        }

        

        
    }
}

