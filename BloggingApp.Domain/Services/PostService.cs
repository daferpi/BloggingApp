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

    public async Task<Post?> GetPostByIdAsync(Guid id)
    {
        return await _postRepository.GetPostByIdAsync(id);
    }

    public async Task<Post?> GetPostByIdWithAuthorDetailsAsync(Guid id)
    {
        return await _postRepository.GetPostByIdWithAuthorDetailsAsync(id);
    }

    public async Task AddPostAsync(Post post)
    {
        await _postRepository.AddPostAsync(post);
    }
}