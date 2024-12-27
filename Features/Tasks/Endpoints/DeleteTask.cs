using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Common.Models;
using TaskManagement.Features.Tasks.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class DeleteTask
    {
        public static void MapDeleteTask(this WebApplication app)
        {
            app.MapDelete("/tasks/{id}", async (Guid id, AppDbContext context, IMapper mapper) =>
            {
                try
                {
                    var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
                    if (task == null)
                    {
                        return Results.NotFound(new ResponseInfo<string>
                        {
                            Message = "Not found any task"
                        });
                    }

                    context.Tasks.Remove(task);
                    await context.SaveChangesAsync();

                    return Results.Ok(new ResponseInfo<TaskResponse>
                    {
                        Message = "Delete tasks succesfully",
                        Info = mapper.Map<TaskResponse>(task)
                    });
                }
                catch (Exception e)
                {
                    return Results.BadRequest(new ResponseInfo<string>
                    {
                        Message = $"Failed to delete: {e.Message}",
                    });
                }
            }).WithName("DeleteTask").WithTags("Tasks").WithSummary("Delete task by id").WithOpenApi().Produces<ResponseInfo<TaskResponse>>();
        }
    }
}
