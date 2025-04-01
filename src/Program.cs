using System.Collections.Concurrent;
using System.Text.Json;

var app = WebApplication.Create(args);

var urlMap = new ConcurrentDictionary<int, string>();
var counter = 1;
var dataFile = "urls.json";

// Load existing data if available
if (File.Exists(dataFile))
{
    try
    {
        var json = File.ReadAllText(dataFile);
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        
        if (data != null)
        {
            counter = Convert.ToInt32(data["counter"]);
            var urls = JsonSerializer.Deserialize<Dictionary<string, string>>(data["urls"].ToString());
            
            if (urls != null)
            {
                foreach (var pair in urls)
                {
                    urlMap.TryAdd(int.Parse(pair.Key), pair.Value);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading data: {ex.Message}");
    }
}

app.MapGet("/shorten", (string url, HttpContext ctx) =>
{
    var id = Interlocked.Increment(ref counter);
    urlMap.TryAdd(id, url);
    
    // Save data after adding a new URL
    SaveData();
    
    return Results.Content($"{ctx.Request.Scheme}://{ctx.Request.Host}/{id}");
});

app.MapGet("/{id:int}", (int id) =>
    urlMap.TryGetValue(id, out var originalUrl) ?
        Results.Redirect(originalUrl) :
        Results.NotFound());

// Helper method to save data
void SaveData()
{
    try
    {
        var urls = urlMap.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value);
        var data = new Dictionary<string, object>
        {
            ["counter"] = counter,
            ["urls"] = urls
        };
        
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataFile, json);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving data: {ex.Message}");
    }
}

// Auto-save on shutdown
app.Lifetime.ApplicationStopping.Register(SaveData);

app.Run();
