using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;
using TaskManagement.Presentations.Request;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tasks
{
    public static class GetList
    {
        public static void MapGetTaskList(this WebApplication app)
        {
            app.MapGet("/tasks", async ([AsParameters] GetTasksRequest @params, AppDbContext ctx) =>
            {
                var sortBy = @params.SortBy.ToLower();
                var sortOrder = @params.SortOrder.ToUpper();

                if (sortOrder != "ASC" && sortOrder != "DESC")
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "SortOrder only accept ASC or DESC value!"
                    });
                }

                if (string.IsNullOrEmpty(sortBy))
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "SortBy cannot be null or empty!"
                    });
                }

                var take = @params.Total <= 0 ? 5 : @params.Total;
                var pageSkip = @params.Page <= 0 ? 1 : @params.Page;

                var tasksQuery = ctx.Tasks
                                .Include(t => t.Tags)
                                .Skip((pageSkip - 1) * take)
                                .Take(take)
                                .AsNoTracking();

                Expression<Func<TaskEntity, object>> sortSelector = sortBy switch
                {
                    "title" => task => task.Title,
                    "status" => task => task.Status,
                    "priority" => task => task.Priority,
                    _ => task => task.Name
                };

                tasksQuery = sortOrder == "ASC"
                    ? tasksQuery.OrderBy(sortSelector)
                    : tasksQuery.OrderByDescending(sortSelector);

                var tasks = await tasksQuery.ToListAsync();
                if (tasks.Count == 0)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Empty list!"
                    });
                }

                return Results.Ok(TaskResponse.MapListFrom(tasks));

            }).WithName("GetTaskList").WithTags("Tasks").WithOpenApi();
        }
    }
}
