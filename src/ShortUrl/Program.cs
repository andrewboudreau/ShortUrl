using System.Collections.Concurrent;

var app = WebApplication.CreateBuilder(args).Build();

ConcurrentDictionary<int, string> urlMappings = [];
int nextId = 0;

app.MapGet("/shorten", (string url, HttpContext context) =>
{
    if (string.IsNullOrEmpty(url))
        return Results.BadRequest("URL parameter is required");

    int id = Interlocked.Increment(ref nextId);
    urlMappings[id] = url;

    var shortUrl = $"{context.Request.Scheme}://{context.Request.Host}/{id}";
    return Results.Ok(new { shortUrl, id, url});
});

app.MapGet("/{id}", (string id) =>
{
    if (int.TryParse(id, out int key) && urlMappings.TryGetValue(key, out var longUrl))
        return Results.Redirect(longUrl);

    return Results.NotFound();
});

app.Run();