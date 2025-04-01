using System.Collections.Concurrent;
using System.Text.Json;

var app = WebApplication.Create(args);

var dataFile = "urls.json";
var urlData = LoadData();
var urlMap = urlData.UrlMap;
var counter = urlData.Counter;

app.MapGet("/shorten", (string url, HttpContext ctx) =>
{
    var id = Interlocked.Increment(ref counter);
    urlMap.TryAdd(id, url);
    
    // Save data after adding a new URL
    File.WriteAllText(dataFile, JsonSerializer.Serialize(new UrlData { Counter = counter, UrlMap = urlMap }));
    
    return Results.Content($"{ctx.Request.Scheme}://{ctx.Request.Host}/{id}");
});

app.MapGet("/{id:int}", (int id) =>
    urlMap.TryGetValue(id, out var originalUrl) ?
        Results.Redirect(originalUrl) :
        Results.NotFound());

// Auto-save on shutdown
app.Lifetime.ApplicationStopping.Register(() => 
    File.WriteAllText(dataFile, JsonSerializer.Serialize(new UrlData { Counter = counter, UrlMap = urlMap })));

// Simple data class and loader
UrlData LoadData()
{
    if (File.Exists(dataFile))
    {
        try
        {
            return JsonSerializer.Deserialize<UrlData>(File.ReadAllText(dataFile)) 
                ?? new UrlData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }
    return new UrlData();
}

class UrlData
{
    public int Counter { get; set; } = 1;
    public ConcurrentDictionary<int, string> UrlMap { get; set; } = new();
}

app.Run();
