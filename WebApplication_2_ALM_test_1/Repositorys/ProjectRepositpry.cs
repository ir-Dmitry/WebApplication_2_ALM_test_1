using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class ProjectRepository
    {
        // Поле для хранения ссылки на базу данных
        private readonly Database _database;

        // Конструктор для инициализации репозитория с базой данных
        public ProjectRepository(Database database)
        {
            _database = database;
        }

        // Метод для получения проекта по его идентификатору
        public ProjectIdDto GetProjectById(int projectId)
        {
            // SQL-запрос для получения данных о проекте по идентификатору
            string query = @"SELECT 
                                pt.id_project, 
                                pt.project_name, 
                                pt.date_of_start, 
                                pt.date_of_end, 
                                pt.planed_budget, 
                                SUM(p.salary / wt.work_time * t.task_time) AS task_reward,
                                STRING_AGG(e.employees_name, ', ') AS employees                                    
                            FROM projects AS pt
                            left join steps as s on s.id_project=pt.id_project
                            LEFT JOIN tasks AS t ON t.id_step = s.id_step
                            LEFT JOIN employees AS e ON t.id_employee = e.id_employee
                            LEFT JOIN posts AS p ON p.id_post = e.id_post
                            LEFT JOIN work_time AS wt ON wt.id_work_time = p.id_work_time
                            WHERE pt.id_project = @projectId
                            GROUP BY pt.id_project, pt.project_name, pt.date_of_start, pt.date_of_end, pt.planed_budget";

            // Создаем объект ProjectIdDto для хранения результатов
            var project = new ProjectIdDto();

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@projectId", projectId);

            // Выполняем запрос и читаем результаты
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Заполняем объект ProjectIdDto данными из запроса
                    project = new ProjectIdDto
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
                return project;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения списка идентификаторов проектов с их названиями и количеством задач
        public IEnumerable<ProjectsIdDto> GetIdProjects()
        {
            // SQL-запрос для получения списка идентификаторов проектов с их названиями и количеством задач
            string query = @"SELECT p.id_project, p.project_name, count(task_name) as task_amount
                                    FROM projects as p
                                    left join steps as s on s.id_project=p.id_project
                                    left join tasks as t on t.id_step=s.id_step
                                    group by p.id_project, p.project_name";

            // Создаем список для хранения результатов
            var projects = new List<ProjectsIdDto>();

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Выполняем запрос и читаем результаты
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Создаем новый объект ProjectsIdDto и добавляем его в список
                    var project = new ProjectsIdDto
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
                // Если проекты не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения списка всех проектов с деталями
        public IEnumerable<ProjectDto> GetAllProjects()
        {
            // SQL-запрос для получения списка всех проектов с деталями
            string query = @"SELECT ss.status_name, p.project_name, p.project_description, p.date_of_start, p.date_of_end, p.planed_budget, p.id_project
                                    FROM projects AS p
                                    join _status as ss on ss.id_status=p.id_status";

            // Создаем список для хранения результатов
            var projects = new List<ProjectDto>();

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Выполняем запрос и читаем результаты
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Создаем новый объект ProjectDto и добавляем его в список
                    var project = new ProjectDto
                    {
                        Id = reader.GetInt32(6),
                        Status = reader.GetString(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        StartDate = reader.GetDateTime(3).ToString("dd.MM.yyyy"),
                        EndDate = reader.GetDateTime(4).ToString("dd.MM.yyyy"),
                        PlanedBudget = reader.GetSqlDecimal(5).ToString()
                    };

                    projects.Add(project);
                }
                return projects;
            }
            else
            {
                // Если проекты не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для добавления нового проекта
        public void AddProject(Project project)
        {
            // SQL-запрос для добавления нового проекта
            string query = @"INSERT INTO projects (id_status, project_name, project_description, date_of_start, date_of_end, planed_budget)
                                    VALUES (@Status, @Name, @Description, @StartDate, @EndDate, @PlannedBudget)";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@Status", project.IdStatus);
            command.Parameters.AddWithValue("@Name", project.Name);
            command.Parameters.AddWithValue("@Description", project.Description);
            command.Parameters.AddWithValue("@StartDate", project.StartDate);
            command.Parameters.AddWithValue("@EndDate", project.EndDate);
            command.Parameters.AddWithValue("@PlannedBudget", project.PlannedBudget);

            // Выполняем запрос и проверяем количество измененных строк
            int rowsAffected = command.ExecuteNonQuery();

            // Если ни одна строка не была добавлена, выбрасываем исключение
            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные.");
            }
        }

        // Метод для обновления информации о проекте
        public void UpdateProject(int projectId, Project project)
        {
            // SQL-запрос для обновления информации о проекте по его идентификатору
            string query = @"UPDATE projects 
                                    SET id_status = @Status, 
                                        project_name = @Name, 
                                        project_description = @Description, 
                                        date_of_start = @StartDate, 
                                        date_of_end = @EndDate,
                                        planed_budget = @PlannedBudget
                                    WHERE id_project = @ProjectId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@Status", project.IdStatus);
            command.Parameters.AddWithValue("@Name", project.Name);
            command.Parameters.AddWithValue("@Description", project.Description);
            command.Parameters.AddWithValue("@StartDate", project.StartDate);
            command.Parameters.AddWithValue("@EndDate", project.EndDate);
            command.Parameters.AddWithValue("@PlannedBudget", project.PlannedBudget);
            command.Parameters.AddWithValue("@ProjectId", projectId);

            // Выполняем запрос и проверяем количество измененных строк
            int rowsAffected = command.ExecuteNonQuery();

            // Если ни одна строка не была обновлена, выбрасываем исключение
            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные.");
            }
        }

        // Метод для удаления проекта по его идентификатору
        public void DeleteProject(int projectId)
        {
            // SQL-запрос для удаления проекта по его идентификатору
            string query = @"DELETE FROM projects WHERE id_project = @ProjectId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметр к запросу
            command.Parameters.AddWithValue("@ProjectId", projectId);

            // Выполняем запрос и проверяем количество удаленных строк
            int rowsAffected = command.ExecuteNonQuery();

            // Если ни одна строка не была удалена, выбрасываем исключение
            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}

