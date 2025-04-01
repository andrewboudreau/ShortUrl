using System.Collections.Concurrent;

var app = WebApplication.Create(args);

var urlMap = new ConcurrentDictionary<int, string>();
var counter = 1;

app.MapGet("/shorten", (string url, HttpContext ctx) =>
{
    var id = Interlocked.Increment(ref counter);
    urlMap.TryAdd(id, url);
    return Results.Content($"{ctx.Request.Scheme}://{ctx.Request.Host}/{id}");
});

app.MapGet("/{id:int}", (int id) =>
    urlMap.TryGetValue(id, out var originalUrl) ?
        Results.Redirect(originalUrl) :
        Results.NotFound());

app.Run();
