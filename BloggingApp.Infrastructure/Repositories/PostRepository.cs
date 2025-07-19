using BloggingApp.Domain.Entities;
using BloggingApp.Domain.Repositories;
using BloggingApp.Infrastructure.Data;

public class PostRepository : IPostRepository
{
    private readonly BlogDBContext _context;
    public PostRepository(BlogDBContext context)
    {
        _context = context;
    }
    public async Task<Post?> GetPostByIdAsync(Guid id)
    {
        // Logic to retrieve a post by ID
        return await _context.Posts.FindAsync(id);
    }

    public Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(Guid authorId)
    {
        // Logic to retrieve posts by author ID
        return Task.FromResult(_context.Posts.Where(p => p.AuthorId == authorId).AsEnumerable());
    }

    public Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        // Logic to retrieve all posts
        return Task.FromResult(_context.Posts.AsEnumerable());
    }

    public Task AddPostAsync(Post post)
    {
        // Logic to add a new post
        _context.Posts.Add(post);
        return _context.SaveChangesAsync();
    }
}