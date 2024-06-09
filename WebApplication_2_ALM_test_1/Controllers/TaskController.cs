using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
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
    }
}

