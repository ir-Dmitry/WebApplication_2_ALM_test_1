using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class ProfileRepository
    {
        private readonly Database _database;

        public ProfileRepository(Database database)
        {
            _database = database;
        }

        public ProfileDto GetProfileByEmployeeId(int employeeId)
        {
            string query = @"SELECT e.employees_name, p.post_name, e.phone_number, e.email
                            FROM employees as e
                            join posts as p on p.id_post = e.id_post
                            where e.id_employee = @employeeId";

            var project = new ProfileDto();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@employeeId", employeeId);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    project = new ProfileDto
                    {
                        EmployeeName = reader.GetString(0),
                        Post = reader.GetString(1),
                        PhoneNumber = reader.GetString(2),
                        Email = reader.GetString(3),
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


        public IEnumerable<TaskIdDto> GetProfileStatusTask()
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

        public IEnumerable<ProfileTaskDto> GetProfileTask(int employeeId)
        {
            string query = @"select t.task_name, p.project_name, ss.status_name
							from tasks as t
							join _status as ss on ss.id_status=t.id_status
							join steps as s on s.id_step=t.id_step
							join projects as p on p.id_project=s.id_project
							where t.id_employee = @employeeId";

            var projects = new List<ProfileTaskDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@employeeId", employeeId);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var project = new ProfileTaskDto
                    {
                        Name = reader.GetString(0),
                        Project = reader.GetString(1),
                        Status = reader.GetString(2)
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
        public (int profile, bool admin) GetProfile(string phoneNumber, int employeeId)
        {
            string query = @"SELECT e.id_employee, e._admin
                    FROM employees as e
                    WHERE e.id_employee = @EmployeeId AND e.phone_number = @PhoneNumber";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int profile = reader.GetInt32(0);
                    bool admin = reader.GetBoolean(1);
                    return (profile, admin);
                }
            }
            throw new Exception("Failed to find the data.");
        }

    }
}
