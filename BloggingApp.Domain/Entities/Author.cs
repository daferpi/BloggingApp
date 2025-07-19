namespace BloggingApp.Domain.Entities;

public class Author(int id, string name="", string surname="")
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Surname { get; private set; } = surname;
}