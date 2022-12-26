using AutoMapper;
using ProjectSetup.Commands;
using ProjectSetup.Domain;

namespace ProjectSetup.Profiles
{
	public class FastPostProfile : Profile
	{
		public FastPostProfile()
		{
			CreateMap<CreateFastPostCommand, FastPost>();
		}
	}
}
