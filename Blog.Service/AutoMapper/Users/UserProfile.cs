using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.ViewModels.Users;

namespace Blog.Service.AutoMapper.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserVM, AppUser>().ReverseMap();
            CreateMap<UserAddVM, AppUser>().ReverseMap();
            CreateMap<UserUpdateVM, AppUser>().ReverseMap();
            CreateMap<UserProfileVM, AppUser>().ReverseMap();
        }
    }
}
