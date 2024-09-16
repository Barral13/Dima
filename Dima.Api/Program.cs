using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
    x => { x.UseSqlServer(cnnStr); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => 
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services
    .AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();



app.MapPost(
    "/v1/categories",
        async (CreateCategoryRequest request, 
        ICategoryHandler handler) 
    => await handler.CreateAsync(request))
        .WithName("Categories: Create")
        .WithSummary("Cria uma nova categoria")
        .Produces<Response<Category?>>();

app.MapPut(
    "/v1/categories/{id:int}",
        async (UpdateCategoryRequest request,
        ICategoryHandler handler,
        int id)
    =>
        {
            request.Id = id;
            return await handler.UpdateAsync(request);
        })
        .WithName("Categories: Update")
        .WithSummary("Atualiza categoria existente")
        .Produces<Response<Category?>>();

app.MapDelete(
    "/v1/categories/{id:int}",
        async (ICategoryHandler handler,
        int id)
    =>
        {
            var request = new DeleteCategoryRequest
            {
                UserId = "02",
                Id = id
            };
            return await handler.DeleteAsync(request);
        })
        .WithName("Categories: Delete")
        .WithSummary("Deleta categoria")
        .Produces<Response<Category?>>();

app.MapGet(
    "/v1/categories/{id:int}",
        async (ICategoryHandler handler,
        int id)
    =>
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = "03",
                Id = id
            };
            return await handler.GetByIdAsync(request);
        })
        .WithName("Categories: Get by Id")
        .WithSummary("Retorna uma categoria")
        .Produces<Response<Category?>>();

app.MapGet(
    "/v1/categories",
        async (ICategoryHandler handler)
    =>
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = "03",
            };
            return await handler.GetAllAsync(request);
        })
        .WithName("Categories: Get All Categories")
        .WithSummary("Retorna todas as categoria do usuário")
        .Produces<PagedResponse<List<Category>?>>();

app.Run();