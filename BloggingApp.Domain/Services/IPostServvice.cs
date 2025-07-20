
using BloggingApp.Domain.Entities;

namespace BloggingApp.Domain.Services
{
    public interface IPostService
    {
        Task<Post?> GetPostByIdAsync(Guid id);
        Task<Post?> GetPostByIdWithAuthorDetailsAsync(Guid id);
        Task AddPostAsync(Post post);
    }
}