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
            _TaskRepository = TaskRepository;
        }

        public IEnumerable<TaskIdDto> GetTaskById(int projectId, int? employeeId = null)
        {
            return _TaskRepository.GetTasksByProjectAndEmployee(projectId, employeeId);
        }

        public Dictionary<int, TaskDto> GetTasksByStepAndEmployee(int stepId, int? employeeId = null)
        {
            var tasks = _TaskRepository.GetTasksByStepAndEmployee(stepId, employeeId);
            return tasks
                .Select((task, index) => new { task, index })
                .ToDictionary(x => x.index + 1, x => x.task);
        }

        // Метод для добавления нового проекта
        public void AddTask(Models.Task task)
        {
            string validationError = task.ValidateDates();
            if (validationError != null)
            {
                throw new ValidationException(validationError);
            }
            _TaskRepository.AddTask(task);
        }

        // Метод для обновления существующего проекта
        public void UpdateTask(int taskId, Models.Task task)
        {
            string validationError = task.ValidateDates();
            if (validationError != null)
            {
                throw new ValidationException(validationError);
            }
            _TaskRepository.UpdateTask(taskId, task);
        }

        // Метод для удаления проекта
        public void DeleteTask(int taskId)
        {
            _TaskRepository.DeleteTask(taskId);
        }
    }
}
