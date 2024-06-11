using System.Collections.Generic;
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



        public IEnumerable<TaskIdDto> GetTaskById()
        {
            return _TaskRepository.GetTaskById();
        }

        public Dictionary<int, TaskDto> GetAllTasks()
        {
            var tasks = _TaskRepository.GetAllTasks();
            return tasks
                .Select((task, index) => new { task, index })
                .ToDictionary(x => x.index + 1, x => x.task);
        }

        // Метод для добавления нового проекта
        public void AddTask(Models.Task task)
        {
            _TaskRepository.AddTask(task);
        }

        // Метод для обновления существующего проекта
        public void UpdateTask(int taskId, Models.Task task)
        {
            _TaskRepository.UpdateTask(taskId, task);
        }

        // Метод для удаления проекта
        public void DeleteTask(int taskId)
        {
            _TaskRepository.DeleteTask(taskId);
        }
    }
}
