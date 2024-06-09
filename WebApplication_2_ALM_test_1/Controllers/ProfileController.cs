using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="profileService">Сервис для работы с профилем сотрудника.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public ProfileController(ProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        /// <summary>
        /// Отобразить профиль сотрудника.
        /// </summary>
        [HttpGet]
        [Route("GetProfile/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProfileDto>))]
        public IActionResult GetProfiles(int employeeId)
        {
            var profiles = _profileService.GetProfileByEmployeeId(employeeId);
            return Ok(profiles);
        }
    }
}

