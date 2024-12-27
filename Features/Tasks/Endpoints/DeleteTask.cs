using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class DeleteTask
    {
        public static void MapDeleteTask(this WebApplication app)
        {
            app.MapDelete("/tasks/{id}", async (Guid id, AppDbContext context) =>
            {
                try
                {
                    var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
                    if (task == null)
                    {
                        return Results.NotFound(new BaseResponse<string>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Not found any task"
                        });
                    }

                    context.Tasks.Remove(task);
                    await context.SaveChangesAsync();

                    return Results.Ok(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Delete tasks succesfully"
                    });
                }
                catch (Exception e)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Failed to delete: {e.Message}",
                    });
                }
            }).WithName("DeleteTask").WithTags("Tasks").WithOpenApi();
        }
    }
}
