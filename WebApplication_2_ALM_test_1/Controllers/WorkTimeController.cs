using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkTimeController : ControllerBase
    {
        private readonly WorkTimeService _workTimeService;
        private readonly ILogger<WorkTimeController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="workTimeService">Сервис для работы с нормами рабочего времени.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public WorkTimeController(WorkTimeService workTimeService, ILogger<WorkTimeController> logger)
        {
            _workTimeService = workTimeService;
            _logger = logger;
        }

        /// <summary>
        /// Отобразить нормы рабочего времени. Используется для выпадающего списка.
        /// </summary>
        [HttpGet]
        [Route("GetWorkTime")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkTimeDto>))]
        public IActionResult GetWorkTimes()
        {
            try
            {
                var workTimes = _workTimeService.GetIdWorkTime();
                return Ok(workTimes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка удаления данных: {ex.Message}");
            }
        }
    }
}

