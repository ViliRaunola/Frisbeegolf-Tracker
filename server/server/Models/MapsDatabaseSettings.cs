namespace server.Models;

public class MapsDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string MapsCollectionName { get; set; } = null!;
}
