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
    public async Task<IActionResult> GetPostById(Guid id, [FromQuery(Name = "authorDetail")] bool? authorDetail)
    {
        var post = authorDetail == false ? await _postService.GetPostByIdAsync(id) : await _postService.GetPostByIdWithAuthorDetailsAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    
    [HttpPost("post")]
    public async Task<IActionResult> AddPost([FromBody] AddPostRequest postRequest)
    { 
        if (postRequest == null || string.IsNullOrWhiteSpace(postRequest.Title) || string.IsNullOrWhiteSpace(postRequest.Content))
        {
            return BadRequest("Invalid post data.");
        }

        var post = new Post(Guid.NewGuid(), Guid.NewGuid(), postRequest.Title, postRequest.Description, postRequest.Content);
        if (postRequest.AuthorName != null && postRequest.AuthorSurname != null)
        {
            post.Author = new Author(post.AuthorId, postRequest.AuthorName, postRequest.AuthorSurname);
        }
            
        await _postService.AddPostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }
}