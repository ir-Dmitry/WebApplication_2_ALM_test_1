using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class ProjectRepository
    {
        private readonly Database _database;

        public ProjectRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            var projects = new List<ProjectDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT p.project_name, s.status_name, p.project_description, p.planed_budget
                                  FROM projects AS p
                                  JOIN _status AS s ON s.id_status = p.id_status";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var project = new ProjectDto
                {
                    Name = reader.GetString(0), // Получаем значение столбца "project_name"
                    Status = reader.GetString(1), // Получаем значение столбца "status_name"
                    Description = reader.GetString(2), // Получаем значение столбца "project_description"
                    PlunedBudget = reader.GetSqlDecimal(3).ToString()
                };

                projects.Add(project);
            }
            return projects;
        }
    }
}
