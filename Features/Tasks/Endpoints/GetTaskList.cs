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
    public static class GetTaskList
    {
        public static void MapGetTaskList(this WebApplication app)
        {
            app.MapPost("/tasks", async (GetTasksRequest request, AppDbContext ctx, IMapper mapper) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!"
                    });
                }

                var validationResults = new GetTaskValidator().Validate(request);
                if (!validationResults.IsValid)
                {
                    return Results.BadRequest(new BaseResponse<List<string>>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Invalid request!",
                        Info = validationResults.Errors.Select(error => error.ErrorMessage).ToList()
                    });
                }

                var take = Math.Max(request.Total, 5);
                var page = Math.Max(request.Page - 1, 0) * take;

                Expression<Func<TaskEntity, object>> sortSelector = request.SortBy.ToLower() switch
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
                                .OrderByDirection(sortSelector, request.SortOrder)
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
