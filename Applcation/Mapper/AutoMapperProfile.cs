using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity to DTO
            CreateMap<User, UserDto>();
            // DTO to Entity
            CreateMap<UserDto, User>();

            CreateMap<PostDto, Post>();
            CreateMap<Post, PostDto>();

            CreateMap<NotificationDto, Notification>();
            CreateMap<Notification, NotificationDto>();

            CreateMap<MediaDto, Media>();
            CreateMap<Media, MediaDto>();

            CreateMap<ContractDto, Contract>();
            CreateMap<Contract, ContractDto>();

            CreateMap<ClientReviewMcDto, ClientReviewMc>();
            CreateMap<ClientReviewMc, ClientReviewMcDto>();

            CreateMap<McReviewClientDto, McReviewClient>();
            CreateMap<McReviewClient, McReviewClientDto>();
        }
    }
}
