public class AddPostRequest
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public AddPostRequest(Guid authorId, string title, string description, string content)
    {
        AuthorId = authorId;
        Title = title;
        Description = description;
        Content = content;
    }
}