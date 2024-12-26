﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Features.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class Delete
    {
        public static void MapDeleteTag(this WebApplication app)
        {
            app.MapDelete("/tags/{id}", async (Guid id, AppDbContext context) =>
            {
                var tag = await context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
                if (tag == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Not found tag with id: {id}"
                    });
                }

                context.Tags.Remove(tag);
                await context.SaveChangesAsync();

                return Results.Ok(new BaseResponse<string>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Delete tag successfully!",
                });
            }).WithName("DeleteTag").WithTags("Tags").WithOpenApi();
        }
    }
}