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
            var steps = new List<StepDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT ss.status_name, s.step_name, s.step_description, s.date_of_start, s.date_of_end, s.planed_budget, s.id_step
                                    FROM steps AS s
                                    join _status as ss on ss.id_status=s.id_status";
            using var reader = command.ExecuteReader();


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

        public IEnumerable<StepIdDto> GetIdSteps(int projectId)
        {
            var steps = new List<StepIdDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT s.id_step, s.step_name
                                    FROM steps AS s
                                    join projects as p on p.id_project=s.id_project
                                    where p.id_project = @projectId";
            command.Parameters.AddWithValue("@projectId", projectId);

            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var step = new StepIdDto
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };

                steps.Add(step);
            }
            return steps;
        }
    }
}
