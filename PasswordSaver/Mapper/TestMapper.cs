using AutoMapper;
using PasswordSaver.Entities;
using PasswordSaver.Models.User;

namespace PasswordSaver.Mapper
{
    public class TestMapper : Profile
    {
        public TestMapper()
        {
            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>();
        }
    }
}
