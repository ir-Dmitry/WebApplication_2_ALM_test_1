using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class EmployeeRepository
    {
        private readonly Database _database;

        public EmployeeRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<EmployeeIdDto> GetIdEmployee()
        {
            string query = @"SELECT id_employee, employees_name
                                    FROM employees";

            var employees = new List<EmployeeIdDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
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
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }


        public IEnumerable<EmployeeDto> GetAllEmployee()
        {
            string query = @"SELECT e.employees_name, o.organisation_name, p.post_name, e.phone_number, e.email, e.id_employee
                            FROM employees as e
                            join posts as p on p.id_post = e.id_post
                            join organisations as o on o.id_organisation = e.id_organisation";

            var employees = new List<EmployeeDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
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
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public void AddEmployee(Employee employee)
        {
            string query = @"INSERT INTO employees (e.employees_name, e.id_organisation, e.id_post, e.phone_number, e.email)
                                    VALUES (@EmployeeName, @IdOrganisation, @IdPost, @PhoneNumber, @Email)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            command.Parameters.AddWithValue("@IdOrganisation", employee.IdOrganisation);
            command.Parameters.AddWithValue("@IdPost", employee.IdPost);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Email", employee.Email);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные.");
            }
        }

        public void UpdateEmployee(int employeeId, Employee employee)
        {
            string query = @"UPDATE employees 
                                    SET id_organisation = @IdOrganisation, 
                                        id_post = @IdPost, 
                                        employees_name = @EmployeeName, 
                                        phone_number = @PhoneNumber, 
                                        email= @Email
                                    WHERE id_employee = @EmployeeId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
            command.Parameters.AddWithValue("@IdOrganisation", employee.IdOrganisation);
            command.Parameters.AddWithValue("@IdPost", employee.IdPost);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Email", employee.Email);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные.");
            }
        }


        public void DeleteEmployee(int employeeId)
        {
            string query = @"DELETE FROM employees WHERE id_employee = @EmployeeId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
