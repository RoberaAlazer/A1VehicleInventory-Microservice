using ApiGateway.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapReverseProxy();

app.Run();