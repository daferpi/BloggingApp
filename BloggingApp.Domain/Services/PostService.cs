using BloggingApp.Domain.Entities;
using BloggingApp.Domain.Services;
using BloggingApp.Domain.Repositories;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _postRepository.GetPostByIdAsync(id);
    }

    public async Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(int authorId)
    {
        return await _postRepository.GetPostsByAuthorIdAsync(authorId);
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _postRepository.GetAllPostsAsync();
    }

    public async Task AddPostAsync(Post post)
    {
        await _postRepository.AddPostAsync(post);
    }
}