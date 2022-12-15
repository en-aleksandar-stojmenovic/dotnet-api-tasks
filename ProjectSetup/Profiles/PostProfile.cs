using AutoMapper;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Domain;

namespace ProjectSetup.Profiles
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
