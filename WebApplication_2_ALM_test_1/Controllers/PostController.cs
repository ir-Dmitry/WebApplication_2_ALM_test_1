using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly ILogger<PostController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="postService">Сервис для работы с должностями.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public PostController(PostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        /// <summary>
        /// Получить список должностей.
        /// </summary>
        [HttpGet]
        [Route("GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostDto>))]
        public IActionResult GetPosts()
        {
            var posts = _postService.GetAllPosts();
            return Ok(posts);
        }
    }
}

