using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tasks.Requests;
using TaskManagement.Features.Tasks.Validations;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class UpdateTask
    {
        public static void MapUpdateTask(this WebApplication app)
        {
            app.MapPut("/tasks/{id}", async (Guid id, UpdateTaskRequest request, AppDbContext context, IMapper mapper) =>
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
                        Info = validatorResult.Errors.Select(error => error.ErrorMessage).ToList()
                    });
                }

                var taskExisted = await context.Tasks.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == id);
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
                    List<Tag> tags = [];
                    foreach (var tag in request.Tags)
                    {
                        var tagEntity = new Tag
                        {
                            Name = tag.Name,
                        };

                        if (!tag.Id.HasValue)
                        {
                            context.Tags.Add(tagEntity);
                        }
                        else
                        {
                            tagEntity.Id = tag.Id.Value;
                            context.Tags.Update(tagEntity);
                        }
                        tags.Add(tagEntity);
                    }

                    taskExisted.TaskTags = tags.Select(t => new TaskTag { TaskId = id, TagId = t.Id }).ToList();
                    var taskModified = mapper.Map(request, taskExisted);
                    context.Tasks.Update(taskModified);
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
