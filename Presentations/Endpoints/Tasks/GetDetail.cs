using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tasks
{
    public static class GetDetail
    {
        public static void MapGetTaskDetail(this WebApplication app)
        {
            app.MapGet("/tasks/{id}", async (Guid id, AppDbContext context) =>
            {
                var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Not found any task"
                    });
                }

                return Results.Ok(TaskResponse.MapFrom(task));
            }).WithName("GetById").WithTags("Tasks").WithOpenApi();
        }
    }
}
