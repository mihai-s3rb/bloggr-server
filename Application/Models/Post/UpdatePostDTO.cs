namespace Bloggr.Application.Models.Post
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Caption { get; set; }
    }
}
