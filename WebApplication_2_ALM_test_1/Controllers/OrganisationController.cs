using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganisationController : ControllerBase
    {
        private readonly OrganisationService _organisationService;
        private readonly ILogger<OrganisationController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="organisationService">Сервис для работы с организациями.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public OrganisationController(OrganisationService organisationService, ILogger<OrganisationController> logger)
        {
            _organisationService = organisationService;
            _logger = logger;
        }

        /// <summary>
        /// Получить список организаций.
        /// </summary>
        [HttpGet]
        [Route("GetOrganisation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganisationDto>))]
        public IActionResult GetOrganisations()
        {
            var organisations = _organisationService.GetAllOrganisations();
            return Ok(organisations);
        }
    }
}

