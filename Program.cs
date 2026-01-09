using maS = myapp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<maS.ConsoleLogger>();
builder.Services.AddSingleton<maS.DatabaseLogger>();
builder.Services.AddSingleton<maS.ILogger>(sp =>
{
    return new maS.MultiLogger(new maS.ILogger[]
    {
        sp.GetRequiredService<maS.ConsoleLogger>(),
        sp.GetRequiredService<maS.DatabaseLogger>()
    });
});
builder.Services.AddScoped<maS.ITextAnalyzer, maS.TextAnalyzer>();

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
var url = $"http://0.0.0.0:{port}";
var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => $"Hello {target}!");

app.MapControllers();

app.Run(url);
