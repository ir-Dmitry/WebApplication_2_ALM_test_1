﻿using System.Collections.Generic;
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

        // Метод для получения задач по проекту и сотруднику
        public IEnumerable<TaskIdDto> GetTasksByProjectAndEmployee(int projectId, int? employeeId = null)
        {
            string query = @"SELECT t.id_task, t.task_name, ss.status_name, t.date_of_end
                                    FROM tasks as t
                                    join _status as ss on ss.id_status=t.id_status
                                    join steps as s on s.id_step=t.id_step
                                    join projects as p on p.id_project=s.id_project
                                    where p.id_project=@ProjectId";

            // Добавление фильтра по сотруднику, если employeeId передан
            if (employeeId.HasValue && employeeId > 0)
            {
                query += " AND t.id_employee = @employeeId";
            }

            var tasks = new List<TaskIdDto>();

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = query; // Установка текста запроса

            if (employeeId.HasValue && employeeId > 0)
            {
                command.Parameters.AddWithValue("@employeeId", employeeId.Value); // Добавление параметра employeeId
            }

            command.Parameters.AddWithValue("@ProjectId", projectId); // Добавление параметра projectId

            using var reader = command.ExecuteReader(); // Выполнение запроса и получение результатов

            if (reader.HasRows) // Проверка наличия данных
            {
                while (reader.Read()) // Чтение данных из результата запроса
                {
                    var task = new TaskIdDto // Создание объекта TaskIdDto для хранения данных о задаче
                    {
                        Id = reader.GetInt32(0), // Получение идентификатора задачи из первого столбца
                        Name = reader.GetString(1), // Получение имени задачи из второго столбца
                        Status = reader.GetString(2), // Получение статуса задачи из третьего столбца
                        EndDate = reader.GetDateTime(3).ToString("dd.MM.yyyy") // Получение даты окончания задачи из четвертого столбца
                    };
                    tasks.Add(task); // Добавление задачи в список
                }
                return tasks; // Возврат списка задач
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения задач по этапу и сотруднику
        public IEnumerable<TaskDto> GetTasksByStepAndEmployee(int stepId, int? employeeId = null)
        {
            // Базовый запрос
            string query = @"SELECT t.task_name, st.status_name, t.task_description, e.employees_name, t.date_of_start, t.date_of_end, (p.salary/wt.work_time*t.task_time) as task_reward, t.id_task, e.id_employee
                     FROM tasks as t
                     left JOIN employees as e ON t.id_employee = e.id_employee
                     JOIN _status as st ON t.id_status = st.id_status
                     JOIN posts as p ON p.id_post = e.id_post
                     JOIN work_time as wt ON wt.id_work_time = p.id_work_time
                     WHERE t.id_step = @stepId";

            // Добавление фильтра по сотруднику, если employeeId передан
            if (employeeId.HasValue && employeeId > 0)
            {
                query += " AND e.id_employee = @employeeId";
            }

            var tasks = new List<TaskDto>();

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = query; // Установка текста запроса

            command.Parameters.AddWithValue("@stepId", stepId); // Добавление параметра stepId

            if (employeeId.HasValue && employeeId > 0)
            {
                command.Parameters.AddWithValue("@employeeId", employeeId.Value); // Добавление параметра employeeId
            }

            using var reader = command.ExecuteReader(); // Выполнение запроса и получение результатов

            if (reader.HasRows) // Проверка наличия данных
            {
                while (reader.Read()) // Чтение данных из результата запроса
                {
                    var task = new TaskDto // Создание объекта TaskDto для хранения данных о задаче
                    {
                        Id = reader.GetInt32(7), // Получение идентификатора задачи из восьмого столбца
                        Name = reader.GetString(0), // Получение имени задачи из первого столбца
                        Status = reader.GetString(1), // Получение статуса задачи из второго столбца
                        Description = reader.GetString(2), // Получение описания задачи из третьего столбца
                        Employee = reader.GetString(3), // Получение имени сотрудника из четвертого столбца
                        StartDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"), // Получение даты начала задачи из пятого столбца
                        EndDate = reader.GetDateTime(5).ToString("dd.MM.yyyy"), // Получение даты окончания задачи из шестого столбца
                        Reward = reader.GetInt32(6) // Получение вознаграждения за задачу из седьмого столбца
                    };

                    tasks.Add(task); // Добавление задачи в список
                }
                return tasks; // Возврат списка задач
            }
            else
            {
                throw new Exception("Не удалось найти данные."); // Если данных нет, бросаем исключение
            }
        }

        // Метод для добавления новой задачи
        public void AddTask(Models.Task task)
        {
            string query = @"INSERT INTO tasks (id_step, id_status, id_employee, task_name, task_description, date_of_start, date_of_end, task_time)
                                    VALUES (@IdStep, @IdStatus, @IdEmployee, @Name, @Description, @StartDate, @EndDate, @TaskTime)";

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = query; // Установка текста запроса

            // Добавление параметров для запроса
            command.Parameters.AddWithValue("@IdStep", task.IdStep);
            command.Parameters.AddWithValue("@IdStatus", task.IdStatus);
            command.Parameters.AddWithValue("@IdEmployee", task.IdEmployee);
            command.Parameters.AddWithValue("@Name", task.Name);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@StartDate", task.StartDate);
            command.Parameters.AddWithValue("@EndDate", task.EndDate);
            command.Parameters.AddWithValue("@TaskTime", task.TaskTime);

            int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса на добавление данных в таблицу

            if (rowsAffected == 0) // Проверка количества измененных строк
            {
                throw new Exception("Не удалось добавить данные."); // Если данные не были добавлены, бросаем исключение
            }
        }

        // Метод для обновления задачи по идентификатору задачи
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

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = query; // Установка текста запроса

            // Добавление параметров для запроса
            command.Parameters.AddWithValue("@TaskId", taskId);
            command.Parameters.AddWithValue("@IdStep", task.IdStep);
            command.Parameters.AddWithValue("@Status", task.IdStatus);
            command.Parameters.AddWithValue("@IdEmployee", task.IdEmployee);
            command.Parameters.AddWithValue("@Name", task.Name);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@StartDate", task.StartDate);
            command.Parameters.AddWithValue("@EndDate", task.EndDate);
            command.Parameters.AddWithValue("@TaskTime", task.TaskTime);

            int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса на обновление данных в таблице

            if (rowsAffected == 0) // Проверка количества измененных строк
            {
                throw new Exception("Не удалось обновить данные."); // Если данные не были обновлены, бросаем исключение
            }
        }

        // Метод для обновления задачи по идентификатору задачи сотрудника
        public void UpdateTaskByEmployee(int taskId, Models.Task task)
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

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = query; // Установка текста запроса

            // Добавление параметров для запроса
            command.Parameters.AddWithValue("@TaskId", taskId);
            command.Parameters.AddWithValue("@IdStep", task.IdStep);
            command.Parameters.AddWithValue("@Status", task.IdStatus);
            command.Parameters.AddWithValue("@IdEmployee", task.IdEmployee);
            command.Parameters.AddWithValue("@Name", task.Name);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@StartDate", task.StartDate);
            command.Parameters.AddWithValue("@EndDate", task.EndDate);
            command.Parameters.AddWithValue("@TaskTime", task.TaskTime);

            int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса на обновление данных в таблице

            if (rowsAffected == 0) // Проверка количества измененных строк
            {
                throw new Exception("Не удалось обновить данные."); // Если данные не были обновлены, бросаем исключение
            }
        }

        // Метод для удаления задачи по идентификатору задачи
        public void DeleteTask(int taskId)
        {
            string query = @"DELETE FROM tasks WHERE id_task = @TaskId";

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = query; // Установка текста запроса

            command.Parameters.AddWithValue("@TaskId", taskId); // Добавление параметра taskId

            int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса на удаление данных из таблицы

            if (rowsAffected == 0) // Проверка количества удаленных строк
            {
                throw new Exception("Не удалось удалить данные."); // Если данные не были удалены, бросаем исключение
            }
        }
    }
}

