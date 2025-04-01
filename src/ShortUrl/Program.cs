var app = WebApplication.Create(args);

var urlMap = new Dictionary<int, string>();

app.MapGet("/shorten", (string url, HttpContext ctx) =>
    Results.Content($"{ctx.Request.Scheme}://{ctx.Request.Host}/{urlMap.Count}", urlMap[urlMap.Count] = url));

app.MapGet("/{id:int}", (int id) =>
    Results.Redirect(urlMap[id]));

app.Run();