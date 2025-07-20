namespace BloggingApp.Domain.Entities;

public class Post(Guid id, Guid authorId, string title, string description, string content)
{
    public Guid Id { get; private set; } = id;
    public Guid AuthorId { get; private set; } = authorId;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public string Content { get; private set; } = content;

    public Author? Author { get; set; } = null;
}
