var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
