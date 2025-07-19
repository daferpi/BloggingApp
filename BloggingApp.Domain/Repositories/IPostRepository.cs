using BloggingApp.Domain.Entities;

namespace BloggingApp.Domain.Repositories
{ 
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task AddPostAsync(Post post);
    }
}
