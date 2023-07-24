using AutoMapper;
using Flowingly.Application.Interfaces;
using Flowingly.Application.Mapping;
using Flowingly.Application.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers();
builder.Services.AddScoped<IExtractXmlService, ExtractXmlService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// Configure AutoMapper
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

IMapper mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

// Other services and configurations.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
