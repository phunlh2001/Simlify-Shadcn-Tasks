using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Models;
using TaskManagement.Features.Tasks.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class SearchTask
    {
        public static void MapSearchTask(this WebApplication app)
        {
            app.MapGet("/tasks", async ([AsParameters] SearchTaskRequest query, AppDbContext ctx, IMapper mapper) =>
            {
                var tasks = await ctx.Tasks
                                .Include(task => task.Tags)
                                .Where(t => t.Title.ToLower().Contains(query.Title.ToLower()))
                                .ToListAsync();

                if (tasks.Count == 0)
                {
                    return Results.NotFound(new ResponseInfo<string>
                    {
                        Message = "Not found any task"
                    });
                }

                return Results.Ok(mapper.Map<List<TaskResponse>>(tasks));
            }).WithName("SearchTask").WithTags("Tasks").WithSummary("Search many tasks by title").WithOpenApi().Produces<List<TaskResponse>>();
        }
    }
}
