using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tags.Requests;
using TaskManagement.Features.Tags.Responses;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class GetList
    {
        public static void MapGetTagList(this WebApplication app)
        {
            app.MapPost("/tags", async (GetTagsRequest request, AppDbContext context, IMapper mapper) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!"
                    });
                }

                var take = Math.Max(request.Total, 5);
                var tags = await context.Tags
                                .AsNoTracking()
                                .Skip(request.Skip)
                                .Take(take)
                                .ToListAsync();

                if (tags.Count == 0)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Empty list!"
                    });
                }
                return Results.Ok(mapper.Map<List<TagResponse>>(tags));

            }).WithName("GetTagList").WithTags("Tags").WithOpenApi();
        }
    }
}
