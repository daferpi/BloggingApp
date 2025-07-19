using BloggingApp.Domain.Entities;
using BloggingApp.Domain.Repositories;
using BloggingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public class PostRepository : IPostRepository
{
    private readonly BlogDBContext _context;
    public PostRepository(BlogDBContext context)
    {
        _context = context;
    }
    public Task<Post> GetPostByIdAsync(int id)
    {
        // Logic to retrieve a post by ID
        return _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(int authorId)
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