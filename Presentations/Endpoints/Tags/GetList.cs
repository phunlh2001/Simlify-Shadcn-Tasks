using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Presentations.Request;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tags
{
    public static class GetList
    {
        public static void MapGetTagList(this WebApplication app)
        {
            app.MapPost("/tags", async (GetTagsRequest request, AppDbContext context) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!"
                    });
                }

                var take = request.Total <= 0 ? 5 : request.Total;
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
                return Results.Ok(TagResponse.MapListFrom(tags));

            }).WithName("GetTagList").WithTags("Tags").WithOpenApi();
        }
    }
}
