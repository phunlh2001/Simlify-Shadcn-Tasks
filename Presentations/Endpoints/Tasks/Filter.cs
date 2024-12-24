using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Presentations.Request;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tasks
{
    public static class Filter
    {
        public static void MapFilterTasks(this WebApplication app)
        {
            app.MapGet("/tasks/filter", async ([AsParameters] SearchTaskRequest query, AppDbContext ctx) =>
            {
                var titleLower = query.Title.ToLower();
                var tasks = await ctx.Tasks
                                .Include(task => task.Tags)
                                .Where(t => t.Title.ToLower().Contains(titleLower))
                                .ToListAsync();
                if (tasks.Count == 0)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Not found any task"
                    });
                }

                return Results.Ok(TaskResponse.MapListFrom(tasks));
            }).WithName("SearchTask").WithTags("Tasks").WithOpenApi();
        }
    }
}
