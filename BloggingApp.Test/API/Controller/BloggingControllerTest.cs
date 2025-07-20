using BloggingApp.Domain.Entities;
using BloggingApp.Domain.Repositories;
using BloggingApp.Domain.Services;
using BloggingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class BloggingControllerTest
{
    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;
    private readonly BlogDBContext _context;
    private readonly BloggingController _controller;
    private readonly DbContextOptions<BlogDBContext> options;


    public BloggingControllerTest()
    {
        var builder = new DbContextOptionsBuilder<BlogDBContext>();
        builder.UseInMemoryDatabase("TestBloggingAppDB");
        options = builder.Options;
        _context = new BlogDBContext(options);
        _context.Database.EnsureCreated(); // Ensure the database is created for testing

        _postRepository = new PostRepository(_context);
        _postService = new PostService(_postRepository);
        _controller = new BloggingController(_postService);
    }

    [Fact]
    public async Task GetPostById_ShouldReturnPost_WhenPostExists()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var post = CreatePost(postId, Guid.NewGuid());
        await _postRepository.AddPostAsync(post);

        // Act
        var result = await _controller.GetPostById(postId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPost = Assert.IsType<Post>(okResult.Value);
        Assert.Equal(postId, returnedPost.Id);
    }

    [Fact]
    public async Task GetPostById_ShouldReturnNotFound_WhenPostDoesNotExist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var post = CreatePost(Guid.NewGuid(), Guid.NewGuid());
        await _postRepository.AddPostAsync(post);

        // Act
        var result = await _controller.GetPostById(nonExistentId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task AddPost_ShouldReturnCreated_WhenPostIsValid()
    {
        // Arrange
        var postRequest = new AddPostRequest(
             "New Post",
             "Post Description",
             "Post Content",
            "Author Name",
            "Author Surname"
        );

        // Act
        var result = await _controller.AddPost(postRequest);
        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdPost = Assert.IsType<Post>(createdResult.Value);
        Assert.NotNull(createdPost);
        Assert.Equal(postRequest.Title, createdPost.Title);
    }
    [Fact]
    public async Task AddPost_ShouldReturnBadRequest_WhenPostRequestIsInvalid()
    {
        // Arrange
        var invalidPostRequest = new AddPostRequest(
             "",
             "",
             "",
            null,
            null
        );

        // Act
        var result = await _controller.AddPost(invalidPostRequest);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    private Post CreatePost(Guid id, Guid authorId)
    {
        return new Post(id, authorId, "Test Title", "Test Description", "Test Content")
        {
            Author = new Author(authorId, "Author Name", "Author Surname")
        };
    }
    [Fact]
    public async Task GetPostsByAuthorId_ShouldReturnPostWithAuthor_WhenPostExists()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        var post = new Post(postId, authorId, "Test Title", "Test Description", "Test Content")
        {
            Author = new Author(authorId, "Author Name", "Author Surname")
        };
        await _postRepository.AddPostAsync(post);
        // Act
        var result = await _controller.GetPostsByAuthorId(postId);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPost = Assert.IsType<Post>(okResult.Value);
        Assert.Equal(postId, returnedPost.Id);
        Assert.NotNull(returnedPost.Author);
        Assert.Equal(authorId, returnedPost.Author.Id);
    }
    [Fact]
    public async Task GetPostsByAuthorId_ShouldReturnNotFound_WhenPostDoesNotExist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var post = new Post(Guid.NewGuid(), Guid.NewGuid(), "Test Title", "Test Description", "Test Content");
        await _postRepository.AddPostAsync(post);
       
        // Act
        var result = await _controller.GetPostsByAuthorId(nonExistentId);
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
