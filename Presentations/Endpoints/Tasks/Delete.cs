using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tasks
{
    public static class Delete
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
                        Message = e.Message,
                    });
                }
            }).WithName("DeleteTask").WithTags("Tasks").WithOpenApi();
        }
    }
}
