using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorization();

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
    x => { x.UseSqlServer(cnnStr); });

builder.Services
    .AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services
    .AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new { message = "Ok" });
app.MapEndpoints();

app.Run();
