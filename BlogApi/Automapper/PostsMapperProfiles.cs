using AutoMapper;
using Blog.Domain;
using BlogApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Automapper
{
    public class PostsMapperProfiles : Profile
    {
        public PostsMapperProfiles()
        {
            CreateMap<Post, PostGetDto>();
            CreateMap<PostPostPutDto, Post>();
        }
    }
}
