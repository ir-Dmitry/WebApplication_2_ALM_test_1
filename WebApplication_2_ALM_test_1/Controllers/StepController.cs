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
    public class StepController : ControllerBase
    {
        private readonly StepService _stepService;
        private readonly ILogger<StepController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="stepService">Сервис для работы с этапами.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public StepController(StepService stepService, ILogger<StepController> logger)
        {
            _stepService = stepService;
            _logger = logger;
        }

        /// <summary>
        /// Получить расширенные данные этапа. Для отображения на экране этапа
        /// </summary>
        [HttpGet]
        [Route("GetStepById/{stepId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StepIdDto>))]
        public IActionResult GetStepById(int stepId)
        {
            try
            {
                var steps = _stepService.GetStepById(stepId);
                return Ok(steps);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список этапов. Если не указан Id сотрудника возвращает все этапы на проекте
        /// </summary>
        [HttpGet]
        [Route("GetStepsByProjectAndEmployee/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StepDto>))]
        public IActionResult GetStepsByProjectAndEmployee(int projectId, [FromQuery] int? employeeId = null)
        {
            try
            {
                var steps = _stepService.GetStepsByProjectAndEmployee(projectId, employeeId);
                return Ok(steps);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавить этап. Переделать на зависимый от ID проекта
        /// </summary>
        [HttpPost]
        [Route("AddStep")]
        public IActionResult AddStep([FromBody] Step step)
        {
            try
            {
                _stepService.AddStep(step);
                return Ok("Данные успешно добавлены");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка добавления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить этап.
        /// </summary>
        [HttpPut]
        [Route("UpdateStep")]
        public IActionResult UpdateStep(int stepId, Step step)
        {
            try
            {
                _stepService.UpdateStep(stepId, step);
                return Ok($"Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка обновления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить этап.
        /// </summary>
        [HttpDelete]
        [Route("DeleteStep/{stepId}")]
        public IActionResult DeleteStep(int stepId)
        {
            try
            {
                _stepService.DeleteStep(stepId);
                return Ok($"Данные успешно удалены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка удаления данных: {ex.Message}");
            }
        }
    }
}

