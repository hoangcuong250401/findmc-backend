using Application.Dtos.User;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : BaseService<Post, PostDto>, IPostService
    {
        public PostService(IPostRepository repository) : base(repository)
        {
        }
    }
}
