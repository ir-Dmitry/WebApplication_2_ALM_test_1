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
        /// Получить список задачь(статус, дата) по проекту. Для страницы управление задачами.
        /// </summary>
        [HttpGet]
        [Route("GetTaskById/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskIdDto>))]
        public IActionResult GetTaskById(int projectId, [FromQuery] int? employeeId = null)
        {
            try
            {
                var tasks = _taskService.GetTaskById(projectId, employeeId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список задач под этапом. Все задачи если не указан сотрудник.
        /// </summary>
        [HttpGet]
        [Route("GetTask/{stepId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskDto>))]
        public IActionResult GetTasksByStepAndEmployee(int stepId, [FromQuery] int? employeeId = null)
        {

            try
            {
                var tasks = _taskService.GetTasksByStepAndEmployee(stepId, employeeId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавить задачу.
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
        /// Обновить задачу.
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
        /// Удалить задачу.
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

