using System.Text.Json.Serialization;

namespace FileManagement.Firebase.Models;

public class UploadImageResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("bucket")]
    public string Bucket { get; set; }

    [JsonPropertyName("generation")]
    public string Generation { get; set; }

    [JsonPropertyName("metageneration")]
    public string MetaGeneration { get; set; }

    [JsonPropertyName("downloadTokens")]
    public string DownloadTokens { get; set; }

    [JsonPropertyName("size")]
    public string Size { get; set; }

    [JsonPropertyName("contentType")]
    public string ContentType { get; set; }

    [JsonPropertyName("timeCreated")]
    public DateTime TimeCreated { get; set; }

    [JsonPropertyName("updated")]
    public DateTime Updated { get; set; }

    [JsonPropertyName("md5Hash")]
    public string Md5Hash { get; set; }

    [JsonPropertyName("contentEncoding")]
    public string ContentEncoding { get; set; }

    [JsonPropertyName("contentDisposition")]
    public string ContentDisposition { get; set; }
}