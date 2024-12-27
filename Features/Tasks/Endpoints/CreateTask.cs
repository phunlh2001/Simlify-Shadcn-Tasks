using FluentValidation;
using TaskManagement.Common.Models;
using TaskManagement.Features.Tasks.Models;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class CreateTask
    {
        public static void MapCreateTask(this WebApplication app)
        {
            app.MapPost("/tasks/create", async (CreateTaskRequest request, AppDbContext context, IValidator<CreateTaskRequest> validator) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new ResponseInfo<string>
                    {
                        Message = "Request body is required!"
                    });
                }

                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(new ResponseInfo<List<string>>
                    {
                        Message = "Validation failed!",
                        Info = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
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

                return Results.Ok(new ResponseInfo<string>
                {
                    Message = "Create new task successfully!"
                });
            }).WithName("CreateTask").WithTags("Tasks").WithSummary("Create a new task").WithOpenApi();
        }
    }
}
