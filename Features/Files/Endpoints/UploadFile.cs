using TaskManagement.Common.Models;

namespace TaskManagement.Features.Files.Endpoints
{
    public static class UploadFile
    {
        public static void MapUploadFile(this WebApplication app)
        {
            app.MapPost("/upload", async (HttpRequest request) =>
            {
                if (!request.Form.Files.Any())
                {
                    return Results.BadRequest(new ResponseInfo<string>
                    {
                        Message = "At least one file is required"
                    });
                }

                foreach (var file in request.Form.Files)
                {
                    using var stream = new FileStream(@"Uploads\" + file.FileName, FileMode.Create);
                    file.CopyTo(stream);
                }

                return Results.Ok(new ResponseInfo<string> { Message = "Upload image successfully!" });

            }).Accepts<List<IFormFile>>("multipart/form-data").WithSummary("Upload a file").WithTags("File").WithOpenApi();
        }
    }
}
