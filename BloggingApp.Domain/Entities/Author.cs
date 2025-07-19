namespace BloggingApp.Domain.Entities;

public class Author(Guid id, string name="", string surname="")
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Surname { get; private set; } = surname;
}