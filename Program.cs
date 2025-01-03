using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Features.Files.Endpoints;
using TaskManagement.Features.Files.Filters;
using TaskManagement.Features.Tags.Endpoints;
using TaskManagement.Features.Tags.Models;
using TaskManagement.Features.Tags.Validations;
using TaskManagement.Features.Tasks.Endpoints;
using TaskManagement.Features.Tasks.Models;
using TaskManagement.Features.Tasks.Validations;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<FileUploadOperationFilter>();
});
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

builder.Services.AddScoped<IValidator<CreateTaskRequest>, CreateTaskValidator>();
builder.Services.AddScoped<IValidator<UpdateTaskRequest>, UpdateTaskValidator>();
builder.Services.AddScoped<IValidator<GetTasksRequest>, GetTaskValidator>();
builder.Services.AddScoped<IValidator<GetTagsRequest>, GetTagsValidator>();

builder.Services.AddProblemDetails();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddAntiforgery();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature is not null)
        {
            await context.Response.WriteAsJsonAsync(new
            {
                context.Response.StatusCode,
                contextFeature.Error.Message,
            });
        }
    });
});

// Task endpoints
app.MapGetTaskList();
app.MapGetTaskDetail();
app.MapSearchTask();
app.MapCreateTask();
app.MapUpdateTask();
app.MapDeleteTask();

// Tags endpoints
app.MapGetTagList();
app.MapGetTagDetail();
app.MapDeleteTag();

// Upload file
app.MapUploadFile();

await app.Init();
app.Run();
