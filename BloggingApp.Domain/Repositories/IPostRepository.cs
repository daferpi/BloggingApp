using BloggingApp.Domain.Entities;

namespace BloggingApp.Domain.Repositories
{ 
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(Guid id);

        Task<Post?> GetPostByIdWithAuthorDetailsAsync(Guid id);
     
        Task AddPostAsync(Post post);
    }
}
