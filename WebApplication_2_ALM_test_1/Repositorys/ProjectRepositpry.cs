using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
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
        public ProjectIdDto GetProjectById(int projectId)
        {
            string query = @"SELECT p.id_project, p.project_name
                                    FROM projects as p
                                    where p.id_project=@projectId";

            var project = new ProjectIdDto();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@projectId", projectId);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    project = new ProjectIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
                return project;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public IEnumerable<ProjectIdDto> GetIdProjects()
        {
            string query = @"SELECT p.id_project, p.project_name, count(task_name) as task_amount
                                    FROM projects as p
                                    left join steps as s on s.id_project=p.id_project
                                    left join tasks as t on t.id_step=s.id_step
                                    group by p.id_project, p.project_name";
            
            var projects = new List<ProjectIdDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows) 
            { 
                while (reader.Read())
                {
                    var project = new ProjectIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Amount = reader.GetInt32(2)
                    };
                    projects.Add(project);
                }
                return projects;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }


        public IEnumerable<ProjectDto> GetAllProjects()
        {
            string query = @"SELECT ss.status_name, p.project_name, p.project_description, p.date_of_start, p.date_of_end, p.planed_budget, p.id_project
                                    FROM projects AS p
                                    join _status as ss on ss.id_status=p.id_status";

            var projects = new List<ProjectDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    var project = new ProjectDto
                    {
                        Id = reader.GetInt32(6),
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
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public void AddProject(Project project)
        {
            string query = @"INSERT INTO projects (id_status, project_name, project_description, date_of_start, date_of_end, planed_budget)
                                    VALUES (@Status, @Name, @Description, @StartDate, @EndDate, @PlannedBudget)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@Status", project.IdStatus);
            command.Parameters.AddWithValue("@Name", project.Name);
            command.Parameters.AddWithValue("@Description", project.Description);
            command.Parameters.AddWithValue("@StartDate", project.StartDate);
            command.Parameters.AddWithValue("@EndDate", project.EndDate);
            command.Parameters.AddWithValue("@PlannedBudget", project.PlannedBudget);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные.");
            }
        }

        public void UpdateProject(int projectId, Project project)
        {
            string query = @"UPDATE projects 
                                    SET id_status = @Status, 
                                        project_name = @Name, 
                                        project_description = @Description, 
                                        date_of_start = @StartDate, 
                                        date_of_end = @EndDate,
                                        planed_budget = @PlannedBudget
                                    WHERE id_project = @ProjectId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@Status", project.IdStatus);
            command.Parameters.AddWithValue("@Name", project.Name);
            command.Parameters.AddWithValue("@Description", project.Description);
            command.Parameters.AddWithValue("@StartDate", project.StartDate);
            command.Parameters.AddWithValue("@EndDate", project.EndDate);
            command.Parameters.AddWithValue("@PlannedBudget", project.PlannedBudget);
            command.Parameters.AddWithValue("@ProjectId", projectId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные.");
            }
        }


        public void DeleteProject(int projectId)
        {
            string query = @"DELETE FROM projects WHERE id_project = @ProjectId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@ProjectId", projectId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
