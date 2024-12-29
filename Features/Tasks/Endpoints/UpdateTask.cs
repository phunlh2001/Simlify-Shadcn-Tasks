using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Common.Models;
using TaskManagement.Features.Tasks.Models;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class UpdateTask
    {
        public static void MapUpdateTask(this WebApplication app)
        {
            app.MapPut("/tasks/{id}", async (Guid id, UpdateTaskRequest request, AppDbContext context, IMapper mapper, IValidator<UpdateTaskRequest> validator) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new ResponseInfo<string>
                    {
                        Message = "Request body is required!"
                    });
                }

                var validatorResult = await validator.ValidateAsync(request);
                if (!validatorResult.IsValid)
                {
                    return Results.BadRequest(new ResponseInfo<List<string>>
                    {
                        Message = "Validation failed!",
                        Info = validatorResult.Errors.Select(error => error.ErrorMessage).ToList()
                    });
                }

                var taskExisted = await context.Tasks.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == id);
                if (taskExisted == null)
                {
                    return Results.NotFound(new ResponseInfo<string>
                    {
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

                    taskExisted.TaskTags = tags.Select(t => new TaskTag { TaskId = id, TagId = t.Id }).ToList();
                    var taskModified = mapper.Map(request, taskExisted);
                    context.Tasks.Update(taskModified);
                    await context.SaveChangesAsync();

                    return Results.Ok(new ResponseInfo<UpdateTaskRequest>
                    {
                        Message = "Update task succesfully",
                        Info = request
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new Exception("Failed to update task!");
                }
            }).WithName("UpdateTask").WithTags("Tasks").WithSummary("Update task by id").WithOpenApi().Produces<ResponseInfo<UpdateTaskRequest>>();
        }
    }
}
