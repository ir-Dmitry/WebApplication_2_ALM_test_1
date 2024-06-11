using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;
using System.Threading.Tasks;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class TaskRepository
    {
        private readonly Database _database;

        public TaskRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<TaskIdDto> GetTaskById()
        {
            string query = @"SELECT t.id_task, t.task_name, s.status_name, t.date_of_end
                                    FROM tasks as t
                                    join _status as s on s.id_status=t.id_status";

            var tasks = new List<TaskIdDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var task = new TaskIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Status = reader.GetString(2),
                        EndDate = reader.GetDateTime(3).ToString("dd.MM.yyyy")
                    };
                    tasks.Add(task);
                }
                return tasks;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public IEnumerable<TaskDto> GetAllTasks()
        {
            string query = @"SELECT t.task_name, st.status_name, t.task_description, e.employees_name, t.date_of_start, t.date_of_end, (p.salary/wt.work_time*t.task_time) as task_reward, t.id_task
                                    FROM tasks as t
                                    join employees as e on t.id_employee = e.id_employee
                                    join _status as st on t.id_status=st.id_status
                                    join posts as p on p.id_post=e.id_post
                                    join work_time as wt on wt.id_work_time=p.id_work_time";

            var tasks = new List<TaskDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    var task = new TaskDto
                    {
                        Id = reader.GetInt32(7),
                        Name = reader.GetString(0), // Получаем значение столбца "Task_name"
                        Status = reader.GetString(1), // Получаем значение столбца "status_name"
                        Description = reader.GetString(2), // Получаем значение столбца "Task_description"
                        Employee = reader.GetString(3),
                        StartDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"),
                        EndDate = reader.GetDateTime(5).ToString("dd.MM.yyyy"),
                        Reward = reader.GetInt32(6)
                    };

                    tasks.Add(task);
                }
                return tasks;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public void AddTask(Models.Task task)
        {
            string query = @"INSERT INTO tasks (id_step, id_status, id_employee, task_name, task_description, date_of_start, date_of_end, task_time)
                                    VALUES (@IdStep, @IdStatus, @IdEmployee, @Name, @Description, @StartDate, @EndDate, @TaskTime)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@IdStep", task.IdStep);
            command.Parameters.AddWithValue("@IdStatus", task.IdStatus);
            command.Parameters.AddWithValue("@IdEmployee", task.IdEmployee);
            command.Parameters.AddWithValue("@Name", task.Name);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@StartDate", task.StartDate);
            command.Parameters.AddWithValue("@EndDate", task.EndDate);
            command.Parameters.AddWithValue("@TaskTime", task.TaskTime);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные.");
            }
        }

        public void UpdateTask(int taskId, Models.Task task)
        {
            string query = @"UPDATE tasks 
                                    SET id_status = @Status, 
                                        id_step = @IdStep,
                                        id_employee = @IdEmployee,
                                        task_name = @Name, 
                                        task_description = @Description, 
                                        date_of_start = @StartDate, 
                                        date_of_end = @EndDate,
                                        task_time = @TaskTime
                                    WHERE id_task = @TaskId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@TaskId", taskId);
            command.Parameters.AddWithValue("@IdStep", task.IdStep);
            command.Parameters.AddWithValue("@Status", task.IdStatus);
            command.Parameters.AddWithValue("@IdEmployee", task.IdEmployee);
            command.Parameters.AddWithValue("@Name", task.Name);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@StartDate", task.StartDate);
            command.Parameters.AddWithValue("@EndDate", task.EndDate);
            command.Parameters.AddWithValue("@TaskTime", task.TaskTime);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные.");
            }
        }


        public void DeleteTask(int taskId)
        {
            string query = @"DELETE FROM tasks WHERE id_task = @TaskId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@TaskId", taskId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
