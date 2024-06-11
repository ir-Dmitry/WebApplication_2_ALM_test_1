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
        [Route("GetProfileByEmployeeId/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileDto))]
        public IActionResult GetProfileByEmployeeId(int employeeId)
        {
            try
            {
                var profiles = _profileService.GetProfileByEmployeeId(employeeId);
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Отобразить статус всех задач.
        /// </summary>
        [HttpGet]
        [Route("GetProfileStatusTask")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskIdDto>))]
        public IActionResult GetProfileStatusTask()
        {
            try
            {
                var profiles = _profileService.GetProfileStatusTask();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Отобразить мои задачи.
        /// </summary>
        [HttpGet]
        [Route("GetProfileTask/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProfileTaskDto>))]
        public IActionResult GetProfileTask(int employeeId)
        {
            try
            {
                var profiles = _profileService.GetProfileTask(employeeId);
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить Id профиля.
        /// </summary>
        [HttpGet]
        [Route("GetProfile/{phoneNumber}/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileIdDto))]

        public IActionResult GetProfile(string phoneNumber, int employeeId)
        {
            try
            {
                var projects = _profileService.GetProfile(phoneNumber, employeeId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }
    }
}

