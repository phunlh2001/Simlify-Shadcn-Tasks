using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManagement.Features.Tags.Endpoints;
using TaskManagement.Features.Tasks.Endpoints;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    var connection = builder.Configuration.GetConnectionString("DbConnect");
    option.UseSqlServer(connection);
});

builder.Services.ConfigureHttpJsonOptions(option =>
{
    option.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Task endpoints
app.MapGetTaskList();
app.MapGetTaskDetail();
app.MapFilterTasks();
app.MapCreateTask();
app.MapUpdateTask();
app.MapDeleteTask();

// Tags endpoints
app.MapGetTagList();
app.MapGetTagDetail();
app.MapDeleteTag();

await app.Init();
app.Run();
