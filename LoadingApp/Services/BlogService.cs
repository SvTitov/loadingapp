using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace LoadingApp.Services
{
    public class BlogService
    {

        BlobServiceClient _client;
        BlobContainerClient _containerClient;
        BlobClient _blobClient;

        string _storageConnectionString = "{set in the Configure your storage connection string section}";
        string _fileName = $"{Guid.NewGuid()}-temp.txt";

        public BlogService()
        {
     
        }

        public async Task Upload()
        {

            string containerName = $"quickstartblobs{Guid.NewGuid()}";

            _client = new BlobServiceClient(_storageConnectionString);
            _containerClient = await _client.CreateBlobContainerAsync(containerName);


            _blobClient = _containerClient.GetBlobClient(_fileName);


            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!")))
            {
                await _containerClient.UploadBlobAsync(_fileName, memoryStream);

            }
        }
    }
}