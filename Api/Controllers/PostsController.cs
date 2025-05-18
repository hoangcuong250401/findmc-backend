﻿using Application.Dtos.User;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Api.Controllers
{
    public class PostsController : BaseController<Post, PostDto, PostPagedRequest>
    {
        public PostsController(IPostService baseService) : base(baseService)
        {
        }
    }
}
