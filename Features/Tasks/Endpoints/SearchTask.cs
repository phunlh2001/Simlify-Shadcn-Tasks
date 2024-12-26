using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tasks.Requests;
using TaskManagement.Features.Tasks.Responses;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class SearchTask
    {
        public static void MapFilterTasks(this WebApplication app)
        {
            app.MapGet("/tasks", async ([AsParameters] SearchTaskRequest query, AppDbContext ctx, IMapper mapper) =>
            {
                var tasks = await ctx.Tasks
                                .Include(task => task.Tags)
                                .Where(t => t.Title.ToLower().Contains(query.Title.ToLower()))
                                .ToListAsync();

                if (tasks.Count == 0)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Not found any task"
                    });
                }

                return Results.Ok(mapper.Map<List<TaskResponse>>(tasks));
            }).WithName("SearchTask").WithTags("Tasks").WithOpenApi();
        }
    }
}
