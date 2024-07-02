using AzurePractice.Models;
using AzurePractice.Repositories;
using AzurePractice.Repositories.Base;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IProductsRepository, ProductsDapperRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run(); 