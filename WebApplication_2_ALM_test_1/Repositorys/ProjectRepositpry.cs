﻿using System.Collections.Generic;
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
            command.CommandText = @"SELECT ss.status_name, p.project_name, p.project_description, p.date_of_start, p.date_of_end, p.planed_budget
                                    FROM projects AS p
                                    join _status as ss on ss.id_status=p.id_status";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var project = new ProjectDto
                {
                    Status = reader.GetString(0),
                    Name = reader.GetString(1), // Получаем значение столбца "project_name"
                    Description = reader.GetString(2),
                    StartDate = reader.GetDateTime(3).ToString("dd.MM.yyyy"),
                    EndDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"),
                    PlunedBudget = reader.GetSqlDecimal(5).ToString()
                };

                projects.Add(project);
            }
            return projects;
        }

        public IEnumerable<ProjectIdDto> GetIdProjects()
        {
            var projects = new List<ProjectIdDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT p.id_project, p.project_name
                                    FROM projects as p";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var project = new ProjectIdDto
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };

                projects.Add(project);
            }
            return projects;
        }
    }
}
