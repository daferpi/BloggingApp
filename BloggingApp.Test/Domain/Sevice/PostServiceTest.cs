using BloggingApp.Domain.Services;
using BloggingApp.Domain.Entities;
using BloggingApp.Domain.Repositories;
using Moq;
using Xunit.Sdk;

public class PostServiceTest
{
    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;
    private readonly Guid _postId = Guid.NewGuid();
    private readonly Guid _authorId = Guid.NewGuid();
    private bool _withAuthorId = true;

    public PostServiceTest()
    {

        var mockRepository = new Mock<IPostRepository>();
        mockRepository.Setup(repo => repo.GetPostByIdAsync(_postId))
            .ReturnsAsync((Guid id) => new Post(_postId, _authorId, "Test Title", "Test Description", "Test Content"));
        mockRepository.Setup(repo => repo.GetPostByIdWithAuthorDetailsAsync(_postId))
            .ReturnsAsync((Guid id) => mockPost(_postId, _authorId));
        mockRepository.Setup(repo => repo.AddPostAsync(It.IsAny<Post>()))
            .Returns(Task.CompletedTask);
        _postRepository = mockRepository.Object;
        _postService = new PostService(_postRepository);

    }

    private Post mockPost(Guid id, Guid authorId)
    {
        if (_withAuthorId)
        { 
            return new Post(_postId, _authorId, "Test Title", "Test Description", "Test Content")
            {
                Author = new Author(_authorId, "Author Name", "Author Surname")
            };
        }

        return new Post(_postId, authorId, "Test Title" + id, "Test Description" + id, "Test Content" + id);
        
    }


    [Fact]
    public async Task GetPostByIdAsync_ShouldReturnPost_WhenPostExists()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var post = CreatePost(_postId, authorId);
        await _postRepository.AddPostAsync(post);

        // Act
        var result = await _postService.GetPostByIdAsync(post.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(post.Id, result.Id);
    }

    [Fact]
    public async Task GetPostByIdAsync_ShouldReturnNull_WhenPostNotExist()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var post = CreatePost(_postId, authorId);
        await _postRepository.AddPostAsync(post);

        // Act
        var result = await _postService.GetPostByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPostByIdWithAuthorDetailsAsync_ShouldReturnPostWithAuthor_WhenPostExists()
    {
        // Arrange
        
        var post = CreatePost(_postId, _authorId);
        post.Author = new Author(_authorId, "Name Author", "Surename Author");
        // Act
        var result = await _postService.GetPostByIdWithAuthorDetailsAsync(post.Id);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(post.Id, result.Id);
        Assert.NotNull(result.Author);
        Assert.Equal(_authorId, result.Author.Id);
    }

    [Fact]
    public async Task GetPostByIdWithAuthorDetailsAsync_ShouldReturnPostWithoutAuthor_WhenAuthorNotExist()
    {
        // Arrange
        var post = CreatePost(_postId, Guid.NewGuid());
        _withAuthorId = false;
        // Act
        var result = await _postService.GetPostByIdWithAuthorDetailsAsync(post.Id);
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