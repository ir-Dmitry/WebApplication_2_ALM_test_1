using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class ProfileRepository
    {
        // Поле для хранения ссылки на базу данных
        private readonly Database _database;

        // Конструктор для инициализации репозитория с базой данных
        public ProfileRepository(Database database)
        {
            _database = database;
        }

        // Метод для получения профиля сотрудника по его идентификатору
        public ProfileDto GetProfileByEmployeeId(int employeeId)
        {
            // SQL-запрос для получения данных о сотруднике и его должности
            string query = @"SELECT e.employees_name, p.post_name, e.phone_number, e.email
                            FROM employees as e
                            join posts as p on p.id_post = e.id_post
                            where e.id_employee = @employeeId";

            // Создаем объект ProfileDto для хранения результатов
            var project = new ProfileDto();

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@employeeId", employeeId);

            // Выполняем запрос и читаем результаты
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Заполняем объект ProfileDto данными из запроса
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
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения списка задач с их статусами
        public IEnumerable<TaskIdDto> GetProfileStatusTask()
        {
            // SQL-запрос для получения списка задач с их статусами
            string query = @"SELECT t.id_task, t.task_name, s.status_name, t.date_of_end
                                    FROM tasks as t
                                    join _status as s on s.id_status=t.id_status";

            // Создаем список для хранения результатов
            var tasks = new List<TaskIdDto>();

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
                    // Создаем новый объект TaskIdDto и добавляем его в список
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
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения списка задач сотрудника
        public IEnumerable<ProfileTaskDto> GetProfileTask(int employeeId)
        {
            // SQL-запрос для получения списка задач сотрудника
            string query = @"select t.task_name, p.project_name, ss.status_name
							from tasks as t
							join _status as ss on ss.id_status=t.id_status
							join steps as s on s.id_step=t.id_step
							join projects as p on p.id_project=s.id_project
							where t.id_employee = @employeeId";

            // Создаем список для хранения результатов
            var projects = new List<ProfileTaskDto>();

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@employeeId", employeeId);

            // Выполняем запрос и читаем результаты
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Создаем новый объект ProfileTaskDto и добавляем его в список
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
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для аутентификации сотрудника
        public (int profile, bool admin) Authorization(string password, string? phoneNumber = null, string? email = null)
        {
            // SQL-запрос для аутентификации сотрудника
            string query = @"SELECT e.id_employee, e._admin
                    FROM employees as e
                    WHERE e.password_hash = @Password";

            // Добавляем условие на основе переданных параметров (телефон или email)
            if (phoneNumber != null)
            {
                query += " AND e.phone_number = @PhoneNumber";
            }
            else if (email != null)
            {
                query += " AND e.email = @Email";
            }
            else
                throw new Exception("Укажите данные авторизации.");

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу в зависимости от типа аутентификации
            if (phoneNumber != null)
            {
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            }
            else if (email != null)
            {
                command.Parameters.AddWithValue("@Email", email);
            }
            else
                throw new Exception("Укажите данные авторизации.");

            // Добавляем параметр для пароля
            command.Parameters.AddWithValue("@Password", password);

            // Выполняем запрос и читаем результаты
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Получаем идентификатор профиля и признак администратора
                    int profile = reader.GetInt32(0);
                    bool admin = reader.GetBoolean(1);
                    return (profile, admin);
                }
            }
            throw new Exception("Авторизация не удалась.");
        }

    }
}
