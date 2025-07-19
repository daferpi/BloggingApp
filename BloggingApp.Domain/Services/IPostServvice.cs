
using BloggingApp.Domain.Entities;

namespace BloggingApp.Domain.Services
{
    public interface IPostService
    {
        Task<Post?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task AddPostAsync(Post post);
    }
}