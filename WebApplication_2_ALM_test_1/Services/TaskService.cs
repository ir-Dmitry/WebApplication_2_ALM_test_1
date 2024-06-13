using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class TaskService
    {
        private readonly TaskRepository _TaskRepository;

        public TaskService(TaskRepository TaskRepository)
        {
            _TaskRepository = TaskRepository; // Инициализация репозитория задач через конструктор
        }

        // Метод для получения задач по идентификатору проекта и сотруднику
        public IEnumerable<TaskIdDto> GetTaskById(int projectId, int? employeeId = null)
        {
            return _TaskRepository.GetTasksByProjectAndEmployee(projectId, employeeId); // Вызов метода репозитория для получения задач
        }

        // Метод для получения задач по идентификатору этапа и сотруднику в виде словаря
        public Dictionary<int, TaskDto> GetTasksByStepAndEmployee(int stepId, int? employeeId = null)
        {
            var tasks = _TaskRepository.GetTasksByStepAndEmployee(stepId, employeeId); // Получение задач через репозиторий

            // Преобразование списка задач в словарь с индексами начиная с 1
            return tasks
                .Select((task, index) => new { task, index })
                .ToDictionary(x => x.index + 1, x => x.task);
        }

        // Метод для добавления новой задачи
        public void AddTask(Models.Task task)
        {
            string validationError = task.ValidateDates(); // Валидация дат задачи
            if (validationError != null)
            {
                throw new ValidationException(validationError); // Бросаем исключение в случае ошибки валидации
            }
            _TaskRepository.AddTask(task); // Вызов метода репозитория для добавления задачи
        }

        // Метод для обновления существующей задачи
        public void UpdateTask(int taskId, Models.Task task)
        {
            string validationError = task.ValidateDates(); // Валидация дат задачи
            if (validationError != null)
            {
                throw new ValidationException(validationError); // Бросаем исключение в случае ошибки валидации
            }
            _TaskRepository.UpdateTask(taskId, task); // Вызов метода репозитория для обновления задачи
        }

        // Метод для удаления задачи по идентификатору
        public void DeleteTask(int taskId)
        {
            _TaskRepository.DeleteTask(taskId); // Вызов метода репозитория для удаления задачи
        }
    }
}
