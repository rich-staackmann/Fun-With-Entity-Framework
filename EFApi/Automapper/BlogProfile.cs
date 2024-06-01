using AutoMapper;

public class BlogProfile : Profile
{
	public BlogProfile()
	{
		CreateMap<Blog, BlogVM>()
			.ReverseMap()
			.AfterMap((bvm, b) => b.Posts.ForEach(x => x.BlogId = bvm.Id));
		CreateMap<Post, PostVM>()
			.ReverseMap();
	}
}
