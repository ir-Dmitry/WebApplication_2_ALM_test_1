using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class EmployeeRepository
    {
        // Поле для хранения ссылки на базу данных
        private readonly Database _database;

        // Конструктор для инициализации репозитория с базой данных
        public EmployeeRepository(Database database)
        {
            _database = database;
        }

        // Метод для получения списка сотрудников с их идентификаторами и именами
        public IEnumerable<EmployeeIdDto> GetIdEmployee()
        {
            // SQL-запрос для получения идентификаторов и имен сотрудников
            string query = @"SELECT id_employee, employees_name
                                    FROM employees";

            // Создаем список для хранения результатов
            var employees = new List<EmployeeIdDto>();

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
                    // Создаем новый объект EmployeeIdDto и добавляем его в список
                    var employee = new EmployeeIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    employees.Add(employee);
                }
                return employees;
            }
            else
            {
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения всех данных о сотрудниках
        public IEnumerable<EmployeeDto> GetAllEmployee()
        {
            // SQL-запрос для получения полной информации о сотрудниках
            string query = @"SELECT e.employees_name, o.organisation_name, p.post_name, e.phone_number, e.email, e.id_employee
                            FROM employees as e
                            join posts as p on p.id_post = e.id_post
                            join organisations as o on o.id_organisation = e.id_organisation";

            // Создаем список для хранения результатов
            var employees = new List<EmployeeDto>();

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
                    // Создаем новый объект EmployeeDto и добавляем его в список
                    var employee = new EmployeeDto
                    {
                        Id = reader.GetInt32(5),
                        EmployeeName = reader.GetString(0),
                        Organisation = reader.GetString(1),
                        Post = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4)
                    };

                    employees.Add(employee);
                }
                return employees;
            }
            else
            {
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для добавления нового сотрудника
        public void AddEmployee(Employee employee, string PasswordHash)
        {
            // SQL-запрос для вставки нового сотрудника
            string query = @"INSERT INTO employees (e.employees_name, e.id_organisation, e.id_post, e.phone_number, e.email, password_hash)
                                    VALUES (@EmployeeName, @IdOrganisation, @IdPost, @PhoneNumber, @Email, @PasswordHash)";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            command.Parameters.AddWithValue("@IdOrganisation", employee.IdOrganisation);
            command.Parameters.AddWithValue("@IdPost", employee.IdPost);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@PasswordHash", PasswordHash);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось добавить данные, выбрасываем исключение
                throw new Exception("Не удалось добавить данные.");
            }
        }

        // Метод для обновления данных о сотруднике
        public void UpdateEmployee(int employeeId, Employee employee)
        {
            // SQL-запрос для обновления данных о сотруднике
            string query = @"UPDATE employees 
                                    SET id_organisation = @IdOrganisation, 
                                        id_post = @IdPost, 
                                        employees_name = @EmployeeName, 
                                        phone_number = @PhoneNumber, 
                                        email= @Email
                                    WHERE id_employee = @EmployeeId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            command.Parameters.AddWithValue("@IdOrganisation", employee.IdOrganisation);
            command.Parameters.AddWithValue("@IdPost", employee.IdPost);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Email", employee.Email);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось обновить данные, выбрасываем исключение
                throw new Exception("Не удалось обновить данные.");
            }
        }

        // Метод для удаления сотрудника
        public void DeleteEmployee(int employeeId)
        {
            // SQL-запрос для удаления сотрудника
            string query = @"DELETE FROM employees WHERE id_employee = @EmployeeId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметр к запросу
            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось удалить данные, выбрасываем исключение
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
