using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Common.Models;
using TaskManagement.Features.Tags.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class DeleteTag
    {
        public static void MapDeleteTag(this WebApplication app)
        {
            app.MapDelete("/tags/{id}", async (Guid id, AppDbContext context, IMapper mapper) =>
            {
                var tag = await context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
                if (tag == null)
                {
                    return Results.NotFound(new ResponseInfo<string>
                    {
                        Message = $"Not found tag with id: {id}"
                    });
                }

                try
                {
                    context.Tags.Remove(tag);
                    await context.SaveChangesAsync();

                    return Results.Ok(new ResponseInfo<TagResponse>
                    {
                        Message = "Delete tag successfully!",
                        Info = mapper.Map<TagResponse>(tag)
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("Failed to delete tag!");
                }
            }).WithName("DeleteTag").WithTags("Tags").WithSummary("Delete a tag by id").WithOpenApi().Produces<ResponseInfo<TagResponse>>();
        }
    }
}
