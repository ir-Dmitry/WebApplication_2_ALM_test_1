using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="employeeService">Сервис для работы с сотрудниками.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public EmployeeController(EmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Получить список сотрудников.
        /// </summary>
        [HttpGet]
        [Route("GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }
    }
}

