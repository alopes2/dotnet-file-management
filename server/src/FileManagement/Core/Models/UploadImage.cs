namespace FileManagement.Core.Models;

public class UploadImage
{
    public string Name { get; set; }

    public string Extension { get; set; }

    public byte[] File { get; set; }
}