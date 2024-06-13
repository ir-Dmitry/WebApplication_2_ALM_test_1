using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class ProjectService
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // Метод для получения идентификаторов проектов
        public IEnumerable<ProjectsIdDto> GetIdProjects()
        {
            return _projectRepository.GetIdProjects(); // Получение идентификаторов проектов через репозиторий
        }

        // Метод для получения проекта по его идентификатору
        public ProjectIdDto GetProjectById(int projectId)
        {
            return _projectRepository.GetProjectById(projectId); // Получение проекта по его идентификатору через репозиторий
        }

        // Метод для получения всех проектов
        public Dictionary<int, ProjectDto> GetAllProjects()
        {
            var projects = _projectRepository.GetAllProjects(); // Получение всех проектов через репозиторий
            return projects
                .Select((project, index) => new { project, index }) // Преобразование списка проектов в анонимные объекты с индексом
                .ToDictionary(x => x.index + 1, x => x.project); // Преобразование анонимных объектов в словарь, где ключ - индекс + 1, значение - проект
        }

        // Метод для добавления нового проекта
        public void AddProject(Project project)
        {
            string validationError = project.ValidateDates(); // Проверка валидации дат проекта
            if (validationError != null)
            {
                throw new ValidationException(validationError); // Если есть ошибки валидации, выбрасываем исключение ValidationException
            }

            _projectRepository.AddProject(project); // Добавление проекта через репозиторий
        }

        // Метод для обновления данных существующего проекта
        public void UpdateProject(int projectId, Project project)
        {
            string validationError = project.ValidateDates(); // Проверка валидации дат проекта
            if (validationError != null)
            {
                throw new ValidationException(validationError); // Если есть ошибки валидации, выбрасываем исключение ValidationException
            }

            _projectRepository.UpdateProject(projectId, project); // Обновление данных проекта через репозиторий
        }

        // Метод для удаления проекта
        public void DeleteProject(int projectId)
        {
            _projectRepository.DeleteProject(projectId); // Удаление проекта через репозиторий
        }
    }
}
