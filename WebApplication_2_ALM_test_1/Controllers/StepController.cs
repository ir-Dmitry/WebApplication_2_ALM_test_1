using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
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
        /// Получить список этапов.
        /// </summary>
        [HttpGet]
        [Route("GetStep")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StepDto>))]
        public IActionResult GetSteps()
        {
            var steps = _stepService.GetAllSteps();
            return Ok(steps);
        }

        /// <summary>
        /// Получить список этапов.
        /// </summary>
        [HttpGet]
        [Route("GetIdStep/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StepDto>))]
        public IActionResult GetSteps(int projectId)
        {
            var steps = _stepService.GetIdSteps(projectId);
            return Ok(steps);
        }
    }
}

