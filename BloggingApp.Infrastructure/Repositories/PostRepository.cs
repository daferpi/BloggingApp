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

    public async Task<Post?> GetPostByIdWithAuthorDetailsAsync(Guid id)
    {
        // Logic to retrieve a post by ID
        var post = await _context.Posts.FindAsync(id);
        var author = await _context.Authors.FindAsync(post?.AuthorId);
        if (post != null && author != null)
        {
           post.Author = author;
        }
        return post;
    }

    public Task AddPostAsync(Post post)
    {
        // Logic to add a new post
        _context.Posts.Add(post);
        if (post.Author != null)
        {
            _context.Authors.Attach(post.Author);
        }
        return _context.SaveChangesAsync();
    }
}