using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly BloggingContext _context;
    private readonly IMapper _mapper;

    public BlogController(IMapper mapper)
    {

        _context = new BloggingContext();
        _mapper = mapper;
    }

    [HttpGet(Name = "GetBlog")]
    public async Task<BlogVM> Get(int id)
    {
        var blogs = await _context.Blogs
            .Where(b => b.Id == id)
            .Include(p => p.Posts)
            .SingleOrDefaultAsync();

        return _mapper.Map<BlogVM>(blogs);
    }

    [HttpPost(Name = "PostBlog")]
    public async Task<BlogVM> Post(BlogVM blogVM)
    {
        var blog = _mapper.Map<Blog>(blogVM);
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return _mapper.Map<BlogVM>(blog);
    }

    [HttpPut(Name = "PutBlog")]
    public async Task<BlogVM> Put(BlogVM blogVM)
    {
        var blog = _mapper.Map<Blog>(blogVM);
        var existingBlog = await _context.Blogs
            .Include(p => p.Posts)
            .FirstOrDefaultAsync(b => b.Id == blog.Id);

        if (existingBlog == null)
        {
            _context.Add(blog);
        }
        else
        {
            _context.Entry(existingBlog).CurrentValues.SetValues(blog);
            foreach (var post in blog.Posts)
            {
                var existingPost = existingBlog.Posts
                    .FirstOrDefault(p => p.Id == post.Id);
                if (existingPost == null)
                {
                    existingBlog.Posts.Add(post);
                }
                else
                {
                    _context.Entry(existingPost).CurrentValues.SetValues(post);
                }
            }
            foreach (var post in existingBlog.Posts)
            {
                if (!blog.Posts.Any(p => p.Id == post.Id))
                {
                    _context.Remove(post);
                }
            }
        }
        await _context.SaveChangesAsync();
        return _mapper.Map<BlogVM>(blog);
    }

[HttpDelete(Name = "DeleteBlog")]
public async Task Delete(BlogVM blogVM)
{
    var blog = _mapper.Map<Blog>(blogVM);
    _context.Blogs.Remove(blog);
    await _context.SaveChangesAsync();
}
}
