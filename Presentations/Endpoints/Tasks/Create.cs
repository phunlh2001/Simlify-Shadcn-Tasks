using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;
using TaskManagement.Presentations.Request;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tasks
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

                List<Tag> tags = [];
                foreach (var tag in request.Tags)
                {
                    if (!tag.Id.HasValue)
                    {
                        var newTag = new Tag
                        {
                            Id = Guid.NewGuid(),
                            Name = tag.Name,
                        };
                        context.Tags.Add(newTag);
                        tags.Add(newTag);
                    }
                    else
                    {
                        var modifiedTag = new Tag
                        {
                            Id = tag.Id.Value,
                            Name = tag.Name,
                        };
                        context.Tags.Update(modifiedTag);
                        tags.Add(modifiedTag);
                    }
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
