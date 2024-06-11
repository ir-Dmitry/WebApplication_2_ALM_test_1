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

        public IEnumerable<StepDto> GetAllSteps()
        {
            string query = @"SELECT ss.status_name, s.step_name, s.step_description, s.date_of_start, s.date_of_end, s.planed_budget, s.id_step
                                    FROM steps AS s
                                    left join _status as ss on ss.id_status=s.id_status";

            var steps = new List<StepDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

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
                        PlunedBudget = reader.GetSqlDecimal(5).ToString()
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
            string query = @"SELECT s.id_step, s.step_name
                                    FROM steps AS s
                                    where s.id_step = @stepId";

            var steps = new StepIdDto();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@stepId", stepId);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var step = new StepIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
                return steps;
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
