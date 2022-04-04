using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FileManagement.Data.Models;

public class ImageEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; }

    public string Extension { get; set; }

    public string Url { get; set; } = null!;

    // BsonElement is the mapping to the attribute name in MongoDB
    [BsonElement("Created")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}