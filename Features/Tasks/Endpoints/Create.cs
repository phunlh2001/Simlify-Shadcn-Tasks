using System.Net;
using TaskManagement.Features.Models;
using TaskManagement.Features.Tasks.Requests;
using TaskManagement.Features.Tasks.Validations;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class Create
    {
        public static void MapCreateTask(this WebApplication app)
        {
            app.MapPost("/tasks/create", async (CreateTaskRequest request, AppDbContext context) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!"
                    });
                }

                var validator = new CreateTaskValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(new BaseResponse<List<string>>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Validation failed!",
                        Data = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                    });
                }

                List<Tag> tags = [];
                foreach (var tag in request.Tags)
                {
                    var tagEntity = new Tag
                    {
                        Id = tag.Id ?? Guid.NewGuid(),
                        Name = tag.Name,
                    };

                    if (!tag.Id.HasValue)
                    {
                        context.Tags.Add(tagEntity);
                    }
                    else
                    {
                        context.Tags.Update(tagEntity);
                    }

                    tags.Add(tagEntity);
                    await context.SaveChangesAsync();
                }

                var newId = Guid.NewGuid();
                var task = new TaskEntity
                {
                    Id = newId,
                    Name = request.Name,
                    Title = request.Title,
                    Priority = request.Priority,
                    Status = request.Status,
                    TaskTags = tags.Select(tag => new TaskTag
                    {
                        Id = Guid.NewGuid(),
                        TagId = tag.Id,
                        TaskId = newId
                    }).ToList()
                };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();

                return Results.Ok(new BaseResponse<string>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Create new task successfully!"
                });
            }).WithName("CreateTask").WithTags("Tasks").WithOpenApi();
        }
    }
}
