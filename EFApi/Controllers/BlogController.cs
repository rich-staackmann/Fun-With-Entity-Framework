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

    public BlogController(BloggingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetBlog")]
    public async Task<IEnumerable<BlogVM>> Get()
    {
        var blogs = await _context.Blogs
            .Include(p => p.Posts)
            .ThenInclude(a => a.Author)
            .OrderBy( b=> b.BlogId).ToListAsync();

        return _mapper.Map<IEnumerable<BlogVM>>(blogs);
    }

    [HttpPost(Name = "PostBlog")]
    public async Task Post(BlogVM blogVM)
    {
        var blog = _mapper.Map<Blog>(blogVM);
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
    }

    [HttpPut(Name = "PutBlog")]
    public async Task Put(BlogVM blogVM)
    {
        var blog = _mapper.Map<Blog>(blogVM);
        _context.Blogs.Update(blog);
        await _context.SaveChangesAsync();
    }

    [HttpDelete(Name = "DeleteBlog")]
    public async Task Delete(BlogVM blogVM)
    {
        var blog = _mapper.Map<Blog>(blogVM);
        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
    }
}
