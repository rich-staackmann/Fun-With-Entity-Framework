using AutoMapper;

public class BlogProfile : Profile
{
	public BlogProfile()
	{
		CreateMap<Blog, BlogVM>()
			.ReverseMap()
			.AfterMap((bvm, b) => b.Posts.ForEach(x => x.BlogId = bvm.BlogId))
			.AfterMap((bvm, b) => b.Posts.ForEach(x => x.Author.PostId = x.PostId));
		CreateMap<Post, PostVM>()
			.ReverseMap();
		CreateMap<Author, AuthorVM>()
			.ReverseMap();
	}
}
