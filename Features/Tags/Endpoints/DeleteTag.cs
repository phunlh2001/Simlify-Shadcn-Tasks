﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Common.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class DeleteTag
    {
        public static void MapDeleteTag(this WebApplication app)
        {
            app.MapDelete("/tags/{id}", async (Guid id, AppDbContext context) =>
            {
                var tag = await context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
                if (tag == null)
                {
                    return Results.NotFound(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Not found tag with id: {id}"
                    });
                }

                try
                {
                    context.Tags.Remove(tag);
                    await context.SaveChangesAsync();

                    return Results.Ok(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Delete tag successfully!",
                    });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Failed to delete: {ex.Message}",
                    });
                }
            }).WithName("DeleteTag").WithTags("Tags").WithSummary("Delete a tag by id").WithOpenApi();
        }
    }
}
