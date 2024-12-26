using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Models;
using TaskManagement.Features.Tasks.Requests;
using TaskManagement.Features.Tasks.Validations;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tasks.Endpoints
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

                var validatorResult = new UpdateTaskValidator().Validate(request);
                if (!validatorResult.IsValid)
                {
                    return Results.BadRequest(new BaseResponse<List<string>>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Validation failed!",
                        Data = validatorResult.Errors.Select(error => error.ErrorMessage).ToList()
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
