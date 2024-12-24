using TaskManagement.Persistences.Entities;

namespace TaskManagement.Presentations.Response
{
    public class TagResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static List<TagResponse> MapListFrom(List<Tag> tags)
        {
            return tags.Select(tag => new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name
            }).ToList();
        }
    }
}
