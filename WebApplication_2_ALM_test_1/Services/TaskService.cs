using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
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

        public Dictionary<int, TaskDto> GetAllTasks()
        {
            var tasks = _TaskRepository.GetAllTasks();
            return tasks
                .Select((task, index) => new { task, index })
                .ToDictionary(x => x.index, x => x.task);
        }
    }
}
