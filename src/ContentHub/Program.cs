using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddAutoMapper();
builder.AddDbContext(builder.Configuration);
builder.AddRepositories();

var app = builder.Build();

app.MapGet("/", async (IArticlesRepository repository) => await repository.CountAsync());

app.Services.EnsureDatabaseCreated();

app.Run();