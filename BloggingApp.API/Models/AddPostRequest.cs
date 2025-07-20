using BloggingApp.Domain.Entities;

public class AddPostRequest
{   
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
     public string? AuthorName { get; set; } = null;
     public string? AuthorSurname { get; set; } = null;



    public AddPostRequest(string title, string description, string content, string? authorName, string? authorSurname)
    {
        Title = title;
        Description = description;
        Content = content;
        AuthorName = authorName;
        AuthorSurname = authorSurname;
    }
}