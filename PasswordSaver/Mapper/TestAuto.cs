using AutoMapper;
using PasswordSaver.Entities;
using PasswordSaver.Models.User;

namespace PasswordSaver.Mapper
{
    public class TestAuto : Profile
    {
        public TestAuto()
        {
            CreateMap<UserEntity,User>();
        }
    }
}
