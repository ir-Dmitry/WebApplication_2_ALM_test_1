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

        public IEnumerable<StepDto> GetStepsByProjectAndEmployee(int projectId, int? employeeId = null)
        {
            string query = @"SELECT ss.status_name, s.step_name, s.step_description, s.date_of_start, s.date_of_end, s.planed_budget, s.id_step
                                    FROM steps AS s
                                    left join _status as ss on ss.id_status=s.id_status
                                    left join tasks as t on t.id_step=s.id_step
                                    left JOIN employees as e ON t.id_employee = e.id_employee
                                    where s.id_project=@projectId";
            
            // Добавление фильтра по сотруднику, если employeeId передан
            if (employeeId.HasValue && employeeId > 0)
            {
                query += " AND e.id_employee = @employeeId";
            }

            var steps = new List<StepDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            
            command.Parameters.AddWithValue("@projectId", projectId);
            if (employeeId.HasValue && employeeId > 0)
            {
                command.Parameters.AddWithValue("@employeeId", employeeId.Value);
            }

            using var reader = command.ExecuteReader();

            if (reader.HasRows) 
            { 
                while (reader.Read())
                {
                    var step = new StepDto
                    {
                        Id = reader.GetInt32(6),
                        Status = reader.GetString(0),
                        Name = reader.GetString(1), // Получаем значение столбца "project_name"
                        Description = reader.GetString(2),
                        StartDate = reader.GetDateTime(3).ToString("dd.MM.yyyy"),
                        EndDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"),
                        PlanedBudget = reader.GetSqlDecimal(5).ToString()
                    };
                    steps.Add(step);
                }
                return steps;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public StepIdDto GetStepById(int stepId)
        {
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

            var step = new StepIdDto();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@stepId", stepId);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    step = new StepIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        StartDate = reader.GetDateTime(2).ToString("dd-MM-yyyy"),
                        EndDate = reader.GetDateTime(3).ToString("dd-MM-yyyy"),
                        PlanedBudget = reader.GetSqlDecimal(4).ToString(),
                        TaskReward = reader.GetInt32(5).ToString(),
                        Employees = reader.GetString(6)
                    };
                }
                return step;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public void AddStep(Step step)
        {
            string query = @"INSERT INTO steps (id_project, id_status, step_name, step_description, date_of_start, date_of_end, planed_budget)
                                    VALUES (@IdProject, @Status, @Name, @Description, @StartDate, @EndDate, @PlannedBudget)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

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
                throw new Exception("Не удалось добавить данные.");
            }
        }

        public void UpdateStep(int stepId, Step step)
        {
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
                throw new Exception("Не удалось обновить данные.");
            }
        }


        public void DeleteStep(int stepId)
        {
            string query = @"DELETE FROM steps WHERE id_step = @stepId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@stepId", stepId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
