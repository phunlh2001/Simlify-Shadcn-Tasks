using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Data;
using TaskManagement.Data.DTOs;

namespace TaskManagement.Endpoints
{
    public static class TagEndpoints
    {
        public static void MapTagEndpoints(this WebApplication app)
        {
            app.MapPost("/tags", async (GetTagsRequest request, AppDbContext context) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new Response<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!"
                    });
                }

                var take = request.Total <= 0 ? 5 : request.Total;
                var tags = await context.Tags
                                .Skip(request.Skip)
                                .Take(take)
                                .AsNoTracking()
                                .ToListAsync();

                if (tags.Count == 0)
                {
                    return Results.NotFound(new Response<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Empty list!"
                    });
                }

                return Results.Ok(new Response<List<TagResponse>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Update tasks succesfully",
                    Data = tags.Select(tag => new TagResponse
                    {
                        Id = tag.Id,
                        Name = tag.Name
                    }).ToList()
                });
            }).WithName("GetTagList").WithTags("Tags").WithOpenApi();

            app.MapDelete("/tags/{id}", async (Guid id, AppDbContext context) =>
            {
                var tag = await context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
                if (tag == null)
                {
                    return Results.BadRequest(new Response<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Not found tag with id: {id}"
                    });
                }

                context.Tags.Remove(tag);
                await context.SaveChangesAsync();

                return Results.Ok(new Response<string>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Delete tag successfully!",
                });
            }).WithName("DeleteTag").WithTags("Tags").WithOpenApi();
        }
    }
}
