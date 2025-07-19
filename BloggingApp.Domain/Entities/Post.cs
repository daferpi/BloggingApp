namespace BloggingApp.Domain.Entities;

public class Post(Guid AuthorId, string title, string description, string content)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid AuthorId { get; private set; } = AuthorId;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public string Content { get; private set; } = content;

    public Author Author => new(AuthorId);
}
