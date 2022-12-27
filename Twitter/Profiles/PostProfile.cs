using AutoMapper;
using Twitter.Contracts.V1.Requests;
using Twitter.Domain;

namespace Twitter.Profiles
{
	public class PostProfile : Profile
	{
		public PostProfile()
		{
			CreateMap<CreatePostRequest, Post>();
			CreateMap<UpdatePostRequest, Post>();
		}
	}
}
