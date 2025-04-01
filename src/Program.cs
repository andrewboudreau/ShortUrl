
var app = WebApplication.Create(args);

UrlData urlData = await UrlData.LoadAsync();
app.Lifetime.ApplicationStopping.Register(async () => await urlData.SaveAsync());

app.MapGet("/shorten", (string url, HttpContext ctx) =>
{
    return urlData.TryAdd(url, out int id) ?
        Results.Content($"{ctx.Request.Scheme}://{ctx.Request.Host}/{id}") :
        Results.BadRequest();
});

app.MapGet("/{id:int}", (int id) =>
    urlData.UrlMap.TryGetValue(id, out var originalUrl) ?
        Results.Redirect(originalUrl) :
        Results.NotFound());

app.Run();
