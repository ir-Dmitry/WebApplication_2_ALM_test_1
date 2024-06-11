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
        /// Получить список сотрудников и их ID. Для вывпадающего списка
        /// </summary>
        [HttpGet]
        [Route("GetIdEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeIdDto>))]
        public IActionResult GetIdEmployee()
        {
            try
            {
                var employees = _employeeService.GetIdEmployee();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список сотрудников.
        /// </summary>
        [HttpGet]
        [Route("GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
        public IActionResult GetEmployee()
        {
            try
            {
                var employees = _employeeService.GetAllEmployee();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка отображения данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавить сотрудника.
        /// </summary>
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                _employeeService.AddEmployee(employee);
                return Ok("Данные успешно добавлены");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка добавления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить сотрудника.
        /// </summary>
        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee(int employeeId, Employee employee)
        {
            try
            {
                _employeeService.UpdateEmployee(employeeId, employee);
                return Ok($"Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка обновления данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить сотрудника.
        /// </summary>
        [HttpDelete]
        [Route("DeleteEmployee/{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            try
            {
                _employeeService.DeleteEmployee(employeeId);
                return Ok($"Данные успешно удалены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка удаления данных: {ex.Message}");
            }
        }
    }
}

