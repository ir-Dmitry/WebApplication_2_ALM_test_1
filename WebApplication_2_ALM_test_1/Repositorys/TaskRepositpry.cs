using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class TaskRepository
    {
        private readonly Database _database;

        public TaskRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<TaskDto> GetAllTasks()
        {
            var Tasks = new List<TaskDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT t.task_name, st.status_name, t.task_description, e.employees_name, t.date_of_start, t.date_of_end, (p.salary/wt.work_time*t.task_time) as task_reward
                                    FROM tasks as t
                                    join employees as e on t.id_employee = e.id_employee
                                    join _status as st on t.id_status=st.id_status
                                    join posts as p on p.id_post=e.id_post
                                    join work_time as wt on wt.id_work_time=p.id_work_time";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var Task = new TaskDto
                {
                    Name = reader.GetString(0), // Получаем значение столбца "Task_name"
                    Status = reader.GetString(1), // Получаем значение столбца "status_name"
                    Description = reader.GetString(2), // Получаем значение столбца "Task_description"
                    Employee = reader.GetString(3),
                    StartDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"),
                    EndDate = reader.GetDateTime(5).ToString("dd.MM.yyyy"),
                    Reward = reader.GetInt32(6)
                };

                Tasks.Add(Task);
            }
            return Tasks;
        }
    }
}
