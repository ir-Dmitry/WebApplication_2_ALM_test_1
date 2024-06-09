/*using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Controllers
{
    /// <summary>
    /// Контроллер для получения информации о прогнозе погоды.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private static readonly string[] Status = new[]
        {
            "Planed", "In progress", "Completed", "Delivered", "Overdue"
        };

        private readonly ILogger<ProjectController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="logger">Экземпляр логгера.</param>
        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Получить список проектов.
        /// </summary>
        /// <returns>Список объектов <see cref="Project"/> с проектами.</returns>
        [HttpGet]
        [Route("GetProject")]
        public IEnumerable<Project> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Project
            {
                //Date = DateTime.Now.AddDays(index),
                Id = index,//Random.Shared.Next(1, 10),
                Name = "Project " + index.ToString(),
                IdStatus = Random.Shared.Next(1, 3),
                Description = Status[Random.Shared.Next(Status.Length)]
            })
            .ToArray();
        }
    }
}*/


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;
using WebApplication_2_ALM_test_1.Services;

namespace WebApplication_2_ALM_test_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="projectService">Сервис для работы с проектами.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public ProjectController(ProjectService projectService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        /// <summary>
        /// Получить список проектов.
        /// </summary>
        [HttpGet]
        [Route("GetIdProjects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectIdDto>))]
        public IActionResult GetIdProjects()
        {
            var projects = _projectService.GetIdProjects();
            return Ok(projects);
        }

        /// <summary>
        /// Получить список проектов.
        /// </summary>
        [HttpGet]
        [Route("GetProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectDto>))]
        public IActionResult GetProjects()
        {
            var projects = _projectService.GetAllProjects();
            return Ok(projects);
        }



        //[HttpPost]
        //[Route("AddProject")]
        //public IActionResult AddProject(Project project)
        //{
        //    try
        //    {
        //        _projectService.AddProject(project);
        //        return Ok("Данные успешно добавлены.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Ошибка при добавлении данных: {ex.Message}");
        //    }
        //}

        [HttpPost]
        [Route("AddProject")]
        public IActionResult AddProject([FromBody] Project project)
        {
            try
            {
                _projectService.AddProject(project);
                return Ok("Проект успешно добавлен");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при добавлении проекта: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateProject")]
        public IActionResult UpdateProject(int projectId, Project project)
        {
            try
            {
                _projectService.UpdateProject(projectId, project);
                return Ok($"Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при обновлении данных: {ex.Message}");
            }
        }





        [HttpDelete]
        [Route("DeleteProject/{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            try
            {
                _projectService.DeleteProject(projectId);
                return Ok($"Данные успешно удалены.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при удалении данных: {ex.Message}");
            }
        }
    }
}

