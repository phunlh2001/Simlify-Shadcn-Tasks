using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Common.Models;
using TaskManagement.Features.Tags.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class GetTagDetail
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
            }).WithName("GetTagById").WithTags("Tags").WithSummary("Get a tag by id").WithOpenApi();
        }
    }
}
