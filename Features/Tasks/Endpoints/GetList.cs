using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using TaskManagement.Features.Common.Extensions;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tasks.Requests;
using TaskManagement.Features.Tasks.Responses;
using TaskManagement.Features.Tasks.Validations;
using TaskManagement.Persistences;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class GetList
    {
        public static void MapGetTaskList(this WebApplication app)
        {
            app.MapGet("/tasks", async ([AsParameters] GetTasksRequest @params, AppDbContext ctx, IMapper mapper) =>
            {
                var validationResults = new GetTaskValidator().Validate(@params);
                if (!validationResults.IsValid)
                {
                    return Results.BadRequest(new BaseResponse<List<string>>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Invalid request!",
                        Info = validationResults.Errors.Select(error => error.ErrorMessage).ToList()
                    });
                }

                var take = Math.Max(@params.Total, 5);
                var page = Math.Max(@params.Page - 1, 0) * take;

                Expression<Func<TaskEntity, object>> sortSelector = @params.SortBy.ToLower() switch
                {
                    "title" => task => task.Title,
                    "status" => task => task.Status,
                    "priority" => task => task.Priority,
                    _ => task => task.Name
                };

                var tasks = await ctx.Tasks
                                .AsNoTracking()
                                .Include(t => t.Tags)
                                .Skip(page)
                                .Take(take)
                                .OrderByDirection(sortSelector, @params.SortOrder)
                                .ToListAsync();

                if (tasks.Count == 0)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Empty list!"
                    });
                }

                return Results.Ok(mapper.Map<List<TaskResponse>>(tasks));

            }).WithName("GetTaskList").WithTags("Tasks").WithOpenApi();
        }
    }
}
