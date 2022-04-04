namespace FileManagement.Data.Configurations;

public class MongoDBOptions
{
    public const string Name = "MongoDB";

    public string ConnectionString { get; set; }

    public string ImagesDatabase { get; set; }

    public string ImagesCollection { get; set; }
}