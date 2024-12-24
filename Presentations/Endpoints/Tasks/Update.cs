using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;
using TaskManagement.Presentations.Request;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tasks
{
    public static class Update
    {
        public static void MapUpdateTask(this WebApplication app)
        {
            app.MapPut("/tasks/{id}", async (Guid id, UpdateTaskRequest request, AppDbContext context) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!"
                    });
                }

                var taskExisted = await context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (taskExisted == null)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Not found any task has id: {id}"
                    });
                }

                try
                {
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
                        }
                        else
                        {
                            var modifiedTag = new Tag
                            {
                                Id = tag.Id.Value,
                                Name = tag.Name,
                            };
                            context.Tags.Update(modifiedTag);
                        }
                        await context.SaveChangesAsync();
                    }

                    taskExisted.Title = request.Title;
                    taskExisted.Name = request.Name;
                    taskExisted.Status = request.Status;
                    taskExisted.Priority = request.Priority;
                    context.Tasks.Update(taskExisted);
                    await context.SaveChangesAsync();

                    return Results.Ok(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Update task succesfully"
                    });
                }
                catch (Exception e)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Failed to update task: {e.Message}"
                    });
                }
            }).WithName("UpdateTask").WithTags("Tasks").WithOpenApi();
        }
    }
}
