using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Persistences;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Presentations.Endpoints.Tags
{
    public static class GetDetail
    {
        public static void MapGetTagDetail(this WebApplication app)
        {
            app.MapGet("/tags/{id}", async (Guid id, AppDbContext context) =>
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

                var response = new TagResponse
                {
                    Id = id,
                    Name = tag.Name,
                };

                return Results.Ok(response);
            }).WithName("GetTagById").WithTags("Tags").WithOpenApi();
        }
    }
}
