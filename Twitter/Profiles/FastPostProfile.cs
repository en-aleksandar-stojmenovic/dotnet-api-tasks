using AutoMapper;
using Twitter.Commands;
using Twitter.Domain;

namespace Twitter.Profiles
{
	public class FastPostProfile : Profile
	{
		public FastPostProfile()
		{
			CreateMap<CreateFastPostCommand, FastPost>();
		}
	}
}
