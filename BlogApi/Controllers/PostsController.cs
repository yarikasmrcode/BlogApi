using AutoMapper;
using Blog.Domain;
using Blog.Domain.Interfaces;
using BlogApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostsRepository _repository;
        private readonly IMapper _mapper;
        public PostsController(IPostsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _repository.GetPosts();
            var postsGetDtos = _mapper.Map<List<PostGetDto>>(posts);

            return Ok(postsGetDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _repository.GetPost(id);

            if (post == null)
            {
                return BadRequest("Post does not exist");
            }

            var postGetDto = _mapper.Map<PostGetDto>(post);

            return Ok(postGetDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostPostPutDto postDto)
        {
            if (ModelState.IsValid)
            {
                var domainPost = _mapper.Map<Post>(postDto);
                domainPost.CreatedAt = DateTime.UtcNow;

                var postGetDto = _mapper.Map<PostGetDto>(domainPost);
                var result = await _repository.Create(domainPost);

                return CreatedAtAction(nameof(GetPost),
               new { id = domainPost.Id }, postGetDto);

            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost([FromBody] PostPostPutDto updatedPost,int id)
        {
            if (ModelState.IsValid)
            {
                var toUpdate = _mapper.Map<Post>(updatedPost);
                toUpdate.Id = id;
                toUpdate.CreatedAt = DateTime.UtcNow;

                var result = await _repository.Update(toUpdate);

                if (result == false)
                {
                    return BadRequest();
                }

                return Ok();
            }

            return BadRequest();
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _repository.Detele(id);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
