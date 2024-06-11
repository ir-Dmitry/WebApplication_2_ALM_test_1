using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
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
        /// Получить список должностей и их ID. Для выпадающего списка
        /// </summary>
        [HttpGet]
        [Route("GetIdPost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostIdDto>))]
        public IActionResult GetIdPost()
        {
            try
            {
                var posts = _postService.GetIdPost();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список должностей.
        /// </summary>
        [HttpGet]
        [Route("GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostDto>))]
        public IActionResult GetPost()
        {
            try
            {
                var posts = _postService.GetAllPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавить должность.
        /// </summary>
        [HttpPost]
        [Route("AddPost")]
        public IActionResult AddPost([FromBody] Post post)
        {
            try
            {
                _postService.AddPost(post);
                return Ok("Данные успешно добавлены");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка добавления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить должность.
        /// </summary>
        [HttpPut]
        [Route("UpdatePost")]
        public IActionResult UpdatePost(int postId, Post post)
        {
            try
            {
                _postService.UpdatePost(postId, post);
                return Ok($"Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка обновления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить должность.
        /// </summary>
        [HttpDelete]
        [Route("DeletePost/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            try
            {
                _postService.DeletePost(postId);
                return Ok($"Данные успешно удалены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка удаления данных: {ex.Message}");
            }
        }
    }
}

