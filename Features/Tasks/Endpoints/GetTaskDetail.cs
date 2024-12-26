using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tasks.Responses;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tasks.Endpoints
{
    public static class GetTaskDetail
    {
        public static void MapGetTaskDetail(this WebApplication app)
        {
            app.MapGet("/tasks/{id}", async (Guid id, AppDbContext context, IMapper mapper) =>
            {
                var task = await context.Tasks.Include(task => task.Tags).FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Not found any task"
                    });
                }

                return Results.Ok(mapper.Map<TaskResponse>(task));
            }).WithName("GetById").WithTags("Tasks").WithOpenApi();
        }
    }
}
