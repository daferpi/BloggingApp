
using BloggingApp.Domain.Entities;

namespace BloggingApp.Domain.Services
{
    public interface IPostService
    {
        Task<Post> GetPostByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(int authorId);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task AddPostAsync(Post post);
    }
}