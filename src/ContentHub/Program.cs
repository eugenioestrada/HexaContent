using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddAutoMapper();
builder.AddDbContext(builder.Configuration);
builder.AddRepositories();

var app = builder.Build();

app.MapGet("/", async (IArticlesRepository articlesRepository, IAuthorsRepository authorsRepository) => 
{
    var articleCount = await articlesRepository.CountAsync();
    var authorCount = await authorsRepository.CountAsync();
    return new { articleCount, authorCount };
});

app.Services.EnsureDatabaseCreated();

app.Run();
