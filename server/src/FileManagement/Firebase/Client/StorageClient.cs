using System.Net.Http.Headers;
using System.Text.Json;
using FileManagement.Core.Services;
using FileManagement.Firebase.Configurations;
using FileManagement.Firebase.Models;
using Microsoft.Extensions.Options;

namespace FileManagement.Firebase.Client;

public class StorageClient : IStorageClient
{
    private readonly StorageOptions _storageOptions;

    private readonly HttpClient _client;

    public StorageClient(HttpClient client, IOptionsSnapshot<StorageOptions> optionsSnapshot)
    {
        _client = client;
        _storageOptions = optionsSnapshot.Value;
    }

    public async Task<string?> UploadImageAsync(byte[] file, string fileName)
    {
        string result = null;

        try
        {
            using var stream = new MemoryStream(file);

            var content = new StreamContent(stream);

            content.Headers.ContentType = new MediaTypeHeaderValue("image/" + fileName.Split(".").Last());

            var escapedPath = Uri.EscapeDataString($"images/{fileName}");

            var url = $"{_storageOptions.Bucket}/o?name={escapedPath}";

            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<UploadImageResponse>(responseData);

                result = $"{_storageOptions.BaseAddress}{_storageOptions.Bucket}/o/{escapedPath}?alt=media&token={data.DownloadTokens}";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }
}