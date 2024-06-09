using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;
        private readonly ILogger<StatusController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="statusService">Сервис для работы с профилем сотрудника.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public StatusController(StatusService statusService, ILogger<StatusController> logger)
        {
            _statusService = statusService;
            _logger = logger;
        }

        /// <summary>
        /// Отобразить профиль сотрудника.
        /// </summary>
        [HttpGet]
        [Route("GetStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StatusDto>))]
        public IActionResult GetStatuss()
        {
            var statuss = _statusService.GetIdStatus();
            return Ok(statuss);
        }
    }
}

