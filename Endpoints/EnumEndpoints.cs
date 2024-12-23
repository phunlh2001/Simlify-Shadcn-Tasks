using System.Reflection;
using TaskManagement.Data;
using TaskManagement.Data.DTOs;

namespace TaskManagement.Endpoints
{
    public static class EnumEndpoints
    {
        public static void MapEnumEndpoints(this WebApplication app)
        {
            app.MapGet("/enums", (AppDbContext context) =>
            {
                var allEnums = GetAllEnums(Assembly.GetExecutingAssembly());

                return Results.Ok(new Response<Dictionary<string, Dictionary<string, int>>>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Get all enums",
                    Data = allEnums
                });
            }).WithName("GetEnums").WithTags("Enums").WithOpenApi();
        }

        public static Dictionary<string, Dictionary<string, int>> GetAllEnums(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.IsEnum)
                .ToDictionary(t => t.Name, t =>
                    Enum.GetNames(t)
                        .Zip(Enum.GetValues(t).Cast<int>(), (Key, Value) => new { Key, Value })
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }
    }
}
