using TaskManagement.Core.Models;
using TaskManagement.Features.Files.Models;

namespace TaskManagement.Features.Files.Endpoints
{
    public static class UploadFile
    {
        public static void MapUploadFile(this WebApplication app)
        {
            app.MapPost("file/upload", (HttpRequest request) =>
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
                    using var stream = new FileStream(@"C:\Windows\Temp\" + file.FileName, FileMode.CreateNew);
                    file.CopyTo(stream);
                }

                return Results.Ok(new ResponseInfo<string> { Message = "Upload image successfully!" });

            }).Accepts<List<IFormFile>>("multipart/form-data").WithSummary("Upload multiple files by multipart/form-data").WithTags("File").WithOpenApi();

            app.MapPost("file/upload/base64", (List<FileRequest> requests) =>
            {
                foreach (var req in requests)
                {
                    byte[] bytes = Convert.FromBase64String(req.Data.Split(',')[1]);

                    var stream = new MemoryStream(bytes);
                    var formFile = new FormFile(stream, 0, bytes.Length, req.FileName, $"{req.FileName}.png")
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "application/json",
                    };

                    using var fileStream = new FileStream(@"C:\Windows\Temp\" + formFile.FileName, FileMode.CreateNew);
                    formFile.CopyTo(fileStream);
                }
                return Results.Ok(new ResponseInfo<string> { Message = "Upload image successfully!" });
            }).WithSummary("Upload a file by base64 string").WithTags("File").WithOpenApi();
        }
    }
}
