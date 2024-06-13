using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class StepRepository
    {
        private readonly Database _database;

        public StepRepository(Database database)
        {
            _database = database;
        }

        // Метод для получения этапов проекта по проекту и сотруднику (если указан)
        public IEnumerable<StepDto> GetStepsByProjectAndEmployee(int projectId, int? employeeId = null)
        {
            // Запрос SQL для выборки данных о этапах проекта
            string query = @"SELECT ss.status_name, s.step_name, s.step_description, s.date_of_start, s.date_of_end, s.planed_budget, s.id_step
                                    FROM steps AS s
                                    LEFT JOIN _status AS ss ON ss.id_status=s.id_status
                                    LEFT JOIN tasks AS t ON t.id_step=s.id_step
                                    LEFT JOIN employees AS e ON t.id_employee = e.id_employee
                                    WHERE s.id_project=@projectId";

            // Добавление фильтра по сотруднику, если employeeId передан
            if (employeeId.HasValue && employeeId > 0)
            {
                query += " AND e.id_employee = @employeeId";
            }

            var steps = new List<StepDto>(); // Создаем список для хранения объектов DTO этапов

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@projectId", projectId);
            if (employeeId.HasValue && employeeId > 0)
            {
                command.Parameters.AddWithValue("@employeeId", employeeId.Value);
            }

            using var reader = command.ExecuteReader();

            // Обработка результатов запроса
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var step = new StepDto
                    {
                        Id = reader.GetInt32(6),                             // Идентификатор этапа
                        Status = reader.GetString(0),                        // Название статуса
                        Name = reader.GetString(1),                          // Название этапа
                        Description = reader.GetString(2),                   // Описание этапа
                        StartDate = reader.GetDateTime(3).ToString("dd.MM.yyyy"), // Дата начала в формате dd.MM.yyyy
                        EndDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"),   // Дата окончания в формате dd.MM.yyyy
                        PlanedBudget = reader.GetSqlDecimal(5).ToString()    // Запланированный бюджет
                    };
                    steps.Add(step); // Добавляем объект DTO в список
                }
                return steps; // Возвращаем список DTO этапов
            }
            else
            {
                throw new Exception("Не удалось найти данные."); // Если результаты запроса отсутствуют, выбрасываем исключение
            }
        }

        // Метод для получения данных о конкретном этапе по его идентификатору
        public StepIdDto GetStepById(int stepId)
        {
            // Запрос SQL для получения данных о конкретном этапе по его идентификатору
            string query = @"SELECT 
                                s.id_step, 
                                s.step_name, 
                                s.date_of_start, 
                                s.date_of_end, 
                                s.planed_budget, 
                                SUM(p.salary / wt.work_time * t.task_time) AS task_reward,
                                STRING_AGG(e.employees_name, ', ') AS employees                                    
                            FROM steps AS s
                            LEFT JOIN tasks AS t ON t.id_step = s.id_step
                            LEFT JOIN employees AS e ON t.id_employee = e.id_employee
                            LEFT JOIN posts AS p ON p.id_post = e.id_post
                            LEFT JOIN work_time AS wt ON wt.id_work_time = p.id_work_time
                            WHERE s.id_step = @stepId
                            GROUP BY s.id_step, s.step_name, s.date_of_start, s.date_of_end, s.planed_budget;";

            var step = new StepIdDto(); // Создаем объект DTO для этапа

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@stepId", stepId);

            using var reader = command.ExecuteReader();

            // Обработка результатов запроса
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    step = new StepIdDto
                    {
                        Id = reader.GetInt32(0),                             // Идентификатор этапа
                        Name = reader.GetString(1),                          // Название этапа
                        StartDate = reader.GetDateTime(2).ToString("dd-MM-yyyy"), // Дата начала в формате dd-MM-yyyy
                        EndDate = reader.GetDateTime(3).ToString("dd-MM-yyyy"),   // Дата окончания в формате dd-MM-yyyy
                        PlanedBudget = reader.GetSqlDecimal(4).ToString(),   // Запланированный бюджет
                        TaskReward = reader.GetInt32(5).ToString(),          // Вознаграждение за задачи
                        Employees = reader.GetString(6)                      // Список сотрудников
                    };
                }
                return step; // Возвращаем DTO объект для этапа
            }
            else
            {
                throw new Exception("Не удалось найти данные."); // Если результаты запроса отсутствуют, выбрасываем исключение
            }
        }

        // Метод для добавления нового этапа проекта в базу данных
        public void AddStep(Step step)
        {
            // Запрос SQL для добавления нового этапа в базу данных
            string query = @"INSERT INTO steps (id_project, id_status, step_name, step_description, date_of_start, date_of_end, planed_budget)
                                    VALUES (@IdProject, @Status, @Name, @Description, @StartDate, @EndDate, @PlannedBudget)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавление параметров запроса
            command.Parameters.AddWithValue("@IdProject", step.IdProject);
            command.Parameters.AddWithValue("@Status", step.IdStatus);
            command.Parameters.AddWithValue("@Name", step.Name);
            command.Parameters.AddWithValue("@Description", step.Description);
            command.Parameters.AddWithValue("@StartDate", step.StartDate);
            command.Parameters.AddWithValue("@EndDate", step.EndDate);
            command.Parameters.AddWithValue("@PlannedBudget", step.PlannedBudget);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные."); // Если добавление не удалось, выбрасываем исключение
            }
        }

        // Метод для обновления данных о конкретном этапе проекта
        public void UpdateStep(int stepId, Step step)
        {
            // Запрос SQL для обновления данных о конкретном этапе проекта по его идентификатору
            string query = @"UPDATE steps 
                                    SET id_project = @IdProject,
                                        id_status = @Status, 
                                        step_name = @Name, 
                                        step_description = @Description, 
                                        date_of_start = @StartDate, 
                                        date_of_end = @EndDate,
                                        planed_budget = @PlannedBudget
                                    WHERE id_step = @StepId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавление параметров запроса
            command.Parameters.AddWithValue("@IdProject", step.IdProject);
            command.Parameters.AddWithValue("@Status", step.IdStatus);
            command.Parameters.AddWithValue("@Name", step.Name);
            command.Parameters.AddWithValue("@Description", step.Description);
            command.Parameters.AddWithValue("@StartDate", step.StartDate);
            command.Parameters.AddWithValue("@EndDate", step.EndDate);
            command.Parameters.AddWithValue("@PlannedBudget", step.PlannedBudget);
            command.Parameters.AddWithValue("@StepId", stepId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные."); // Если обновление не удалось, выбрасываем исключение
            }
        }

        // Метод для удаления конкретного этапа проекта по его идентификатору
        public void DeleteStep(int stepId)
        {
            // Запрос SQL для удаления конкретного этапа проекта по его идентификатору
            string query = @"DELETE FROM steps WHERE id_step = @stepId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавление параметра запроса
            command.Parameters.AddWithValue("@stepId", stepId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные."); // Если удаление не удалось, выбрасываем и
            }
        }
    }
}