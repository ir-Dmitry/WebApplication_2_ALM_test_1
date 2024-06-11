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
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="taskService">Сервис для работы с задачами.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public TaskController(TaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        /// <summary>
        /// Получить название проекта.
        /// </summary>
        [HttpGet]
        [Route("GetTaskById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskIdDto>))]
        public IActionResult GetTaskById()
        {
            try
            {
                var tasks = _taskService.GetTaskById();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список задач.
        /// </summary>
        [HttpGet]
        [Route("GetTask")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskDto>))]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        /// <summary>
        /// Добавить проект.
        /// </summary>
        [HttpPost]
        [Route("AddTask")]
        public IActionResult AddTask([FromBody] Models.Task task)
        {
            try
            {
                _taskService.AddTask(task);
                return Ok("Данные успешно добавлены");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка добавления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить проект.
        /// </summary>
        [HttpPut]
        [Route("UpdateTask")]
        public IActionResult UpdateTask(int taskId, Models.Task task)
        {
            try
            {
                _taskService.UpdateTask(taskId, task);
                return Ok($"Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка обновления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить проект.
        /// </summary>
        [HttpDelete]
        [Route("DeleteTask/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            try
            {
                _taskService.DeleteTask(taskId);
                return Ok($"Данные успешно удалены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка удаления данных: {ex.Message}");
            }
        }
    }
}

