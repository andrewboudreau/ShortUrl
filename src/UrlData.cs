namespace ShortUrl;

public class UrlData
{
    public const string DefaultFileName = "urls.json";

    private int counter = 0;

    public int Counter => counter;

    public ConcurrentDictionary<int, string> UrlMap { get; private set; } = new();

    public bool TryAdd(string url, out int id)
    {
        id = Interlocked.Increment(ref counter);
        return UrlMap.TryAdd(id, url);
    }

    public async Task SaveAsync()
    {
        await File.WriteAllBytesAsync(DefaultFileName, BinaryData.FromObjectAsJson(this));
    }

    public static async Task<UrlData> LoadAsync(string filename = DefaultFileName)
    {
        if (File.Exists(filename))
        {
            try
            {
                return JsonSerializer.Deserialize<UrlData>(await File.ReadAllTextAsync(filename))
                    ?? new UrlData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        return new UrlData();
    }
}