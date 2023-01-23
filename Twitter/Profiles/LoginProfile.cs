using AutoMapper;
using Twitter.Commands;
using Twitter.Contracts.V1.Requests;

namespace Twitter.Profiles
{
	public class LoginProfile : Profile
	{
		public LoginProfile()
		{
			CreateMap<LoginCommand, LoginUserRequest>();
		}
	}
}
