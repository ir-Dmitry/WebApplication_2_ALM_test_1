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
        /// Получить список организаций и их ID. Для выпадающего списка
        /// </summary>
        [HttpGet]
        [Route("GetIdOrganisation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganisationIdDto>))]
        public IActionResult GetIdOrganisation()
        {
            try
            {
                var organisations = _organisationService.GetIdOrganisation();
                return Ok(organisations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список организаций.
        /// </summary>
        [HttpGet]
        [Route("GetOrganisation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganisationDto>))]
        public IActionResult GetOrganisation()
        {
            try
            {
                var organisations = _organisationService.GetAllOrganisations();
                return Ok(organisations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавить организацию.
        /// </summary>
        [HttpPost]
        [Route("AddOrganisation")]
        public IActionResult AddOrganisation([FromBody] Organisation organisation)
        {
            try
            {
                _organisationService.AddOrganisation(organisation);
                return Ok("Данные успешно добавлены");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка добавления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить организацию.
        /// </summary>
        [HttpPut]
        [Route("UpdateOrganisation")]
        public IActionResult UpdateOrganisation(int organisationId, Organisation organisation)
        {
            try
            {
                _organisationService.UpdateOrganisation(organisationId, organisation);
                return Ok($"Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка обновления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить организацию.
        /// </summary>
        [HttpDelete]
        [Route("DeleteOrganisation/{organisationId}")]
        public IActionResult DeleteOrganisation(int organisationId)
        {
            try
            {
                _organisationService.DeleteOrganisation(organisationId);
                return Ok($"Данные успешно удалены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка удаления данных: {ex.Message}");
            }
        }
    }
}

