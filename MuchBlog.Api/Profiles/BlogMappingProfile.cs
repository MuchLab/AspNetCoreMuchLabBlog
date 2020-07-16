using System;
using System.Linq;
using AutoMapper;
using MuchBlog.Api.Dtos;
using MuchBlog.Domain.Dtos;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Api.Profiles
{
    public class BlogMappingProfile:Profile
    {
        public BlogMappingProfile()
        {
            //Dto和Entity映射
            //年龄和出生日期的映射
            CreateMap<User, UserDto>().ForMember(dest => dest.Age,
                config =>
                    config.MapFrom(src => DateTimeOffset.Now.Year - src.BirthDate.Year));
            CreateMap<Essay, EssayDto>().ForMember(dest => dest.Author,
                config =>
                    config.MapFrom(src => src.User.UserName)).ForMember(dest => dest.ClassificationList, 
                config =>
                    config.MapFrom(src => src.EssayClassifications.Select(x => x.Classification.Name)));

            //创建User需要的映射
            CreateMap<UserForCreationDto, User>();

            //更新User需要的映射
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UserForUpdateDto>();
            
            //创建Essay需要的映射
            CreateMap<EssayForCreationDto, Essay>();

            //更新Essay需要的映射
            CreateMap<EssayForUpdateDto, Essay>();
            CreateMap<Essay, EssayForUpdateDto>();

            //文章图片的映射
            CreateMap<EssayImage, EssayImageDto>().ForMember(dest => dest.Location,
                config => 
                    config.MapFrom(src => $"EssayImages/{src.Essay.User.UserName}/{src.FileName}"));

            CreateMap<Classification, ClassificationDto>();
            CreateMap<ClassificationForUpdateDto, Classification>();
            CreateMap<ClassificationForCreateDto, Classification>();
        }
    }
}