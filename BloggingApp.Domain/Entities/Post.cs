namespace BloggingApp.Domain.Entities;

public class Post(int id, int AuthorId,  string title, string description, string content)
{
    public int Id { get; private set; } = id;
    public int AuthorId { get; private set; } = AuthorId;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public string Content { get; private set; } = content;

    public Author Author => new(AuthorId);
}
