﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Common.Models;
using TaskManagement.Features.Tags.Models;
using TaskManagement.Persistences;

namespace TaskManagement.Features.Tags.Endpoints
{
    public static class GetTagList
    {
        public static void MapGetTagList(this WebApplication app)
        {
            app.MapPost("/tags", async (GetTagsRequest request, AppDbContext context, IMapper mapper, IValidator<GetTagsRequest> validator) =>
            {
                if (request == null)
                {
                    return Results.BadRequest(new BaseResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Request body is required!",
                    });
                }

                var validatorResult = await validator.ValidateAsync(request);
                if (!validatorResult.IsValid)
                {
                    return Results.BadRequest(new BaseResponse<List<string>>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Invalid values!",
                        Info = validatorResult.Errors.Select(x => x.ErrorMessage).ToList()
                    });
                }

                var take = Math.Max(request.Total, 5);
                var skip = Math.Max(request.Page - 1, 0) * take;

                var tags = await context.Tags
                                .AsNoTracking()
                                .Skip(skip)
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

            })
                .WithName("GetTagList")
                .WithTags("Tags")
                .WithSummary("Get tag list")
                .WithOpenApi();
        }
    }
}
