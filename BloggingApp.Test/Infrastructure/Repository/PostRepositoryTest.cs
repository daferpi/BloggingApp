using BloggingApp.Domain.Entities;
using BloggingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PostRepositoryTest
{
    private readonly PostRepository _postRepository;
    private readonly BlogDBContext _context;
    private readonly DbContextOptions<BlogDBContext> options;

    public PostRepositoryTest()
    {
        // Initialize the context and repository for testing
        var builder = new DbContextOptionsBuilder<BlogDBContext>();
        builder.UseInMemoryDatabase("TestBloggingAppDB");
        options = builder.Options;
        _context = new BlogDBContext(options);
        _context.Database.EnsureCreated(); // Ensure the database is created for testing

        _postRepository = new PostRepository(_context);
    }

    [Fact]
    public async Task GetPostByIdAsync_ShouldReturnPost_WhenPostExists()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var post = CreatePost(Guid.NewGuid(), authorId);
        await _postRepository.AddPostAsync(post);

        // Act
        var result = await _postRepository.GetPostByIdAsync(post.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(post.Id, result.Id);
    }

    [Fact]
    public async Task GetPostByIdAsync_ShouldReturnNull_WhenPosNotExist()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var post = CreatePost(Guid.NewGuid(), authorId);
        await _postRepository.AddPostAsync(post);

        // Act
        var result = await _postRepository.GetPostByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPostByIdWithAuthorDetailsAsync_ShouldReturnPostWithAuthor_WhenPostExists()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var post = CreatePost(Guid.NewGuid(), authorId);
        post.Author = new Author(authorId, "Name Author", "Surename Author");
        await _postRepository.AddPostAsync(post);
        // Act
        var result = await _postRepository.GetPostByIdWithAuthorDetailsAsync(post.Id);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(post.Id, result.Id);
        Assert.NotNull(result.Author);
        Assert.Equal(authorId, result.Author.Id);
    }


    [Fact]
    public async Task GetPostByIdWithAuthorDetailsAsync_ShouldReturnPostWithoutAuthor_WhenAuthorNotExist()
    {
        // Arrange
        var post = CreatePost(Guid.NewGuid(), Guid.NewGuid());
        await _postRepository.AddPostAsync(post);
        // Act
        var result = await _postRepository.GetPostByIdWithAuthorDetailsAsync(post.Id);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(post.Id, result.Id);
        Assert.Null(result.Author);
    }
   
    private Post CreatePost(Guid id = default, Guid authorId = default)
    {
        var post = new Post(id, authorId, "Test Title" + id, "Test Description" + id, "Test Content" + id);
        return post;
    }
}