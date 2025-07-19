using BloggingApp.Domain.Services;
using BloggingApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BloggingController : ControllerBase
{
    private readonly IPostService _postService;

    public BloggingController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("posts/{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpGet("posts/author/{authorId}")]
    public async Task<IActionResult> GetPostsByAuthorId(Guid authorId)
    {
        var posts = await _postService.GetPostsByAuthorIdAsync(authorId);
        return Ok(posts);
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    [HttpPost("posts")]
    public async Task<IActionResult> AddPost([FromBody] AddPostRequest postRequest)
    { 
        if (postRequest == null || string.IsNullOrWhiteSpace(postRequest.Title) || string.IsNullOrWhiteSpace(postRequest.Content))
        {
            return BadRequest("Invalid post data.");
        }

        var post = new Post(postRequest.AuthorId, postRequest.Title, postRequest.Description, postRequest.Content);
        await _postService.AddPostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }
}