namespace FileManagement.Firebase.Configurations;

public class StorageOptions
{
    public const string Name = "Storage";

    /// <summary>
    /// Firebase storage API base address
    /// </summary>
    public string BaseAddress { get; set; }

    /// <summary>
    /// Your bucket link 'xxx.appspot.com'
    /// </summary>
    public string Bucket { get; set; }
}