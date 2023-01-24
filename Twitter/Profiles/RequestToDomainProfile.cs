using AutoMapper;
using Twitter.Domain;
using Twitter.Queries;

namespace Twitter.Profiles
{
	public class RequestToDomainProfile : Profile
	{
		public RequestToDomainProfile()
		{
			CreateMap<PaginationQuary, PaginationFilter>();
		}
	}
}
