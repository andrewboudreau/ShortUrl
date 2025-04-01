using System.Collections.Concurrent;

var app = WebApplication.Create(args);

var urlMap = new ConcurrentDictionary<int, string>();
var counter = 0;

app.MapGet("/shorten", (string url, HttpContext ctx) =>
{
    var id = Interlocked.Increment(ref counter) - 1;
    urlMap.TryAdd(id, url);
    return Results.Content($"{ctx.Request.Scheme}://{ctx.Request.Host}/{id}");
});

app.MapGet("/{id:int}", (int id) =>
{
    if (urlMap.TryGetValue(id, out var originalUrl))
    {
        return Results.Redirect(originalUrl);
    }
    return Results.NotFound();
});

app.Run();
