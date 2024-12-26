using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tags.Responses;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class GetDetail
    {
        public static void MapGetTagDetail(this WebApplication app)
        {
            app.MapGet("/tags/{id}", async (Guid id, AppDbContext context, IMapper mapper) =>
            {
                var tag = await context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
                if (tag == null)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Not found any tags"
                    });
                }

                return Results.Ok(mapper.Map<TagResponse>(tag));
            }).WithName("GetTagById").WithTags("Tags").WithOpenApi();
        }
    }
}
