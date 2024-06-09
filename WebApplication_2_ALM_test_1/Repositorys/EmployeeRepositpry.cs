using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class EmployeeRepository
    {
        private readonly Database _database;

        public EmployeeRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = new List<EmployeeDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT e.employees_name, o.organisation_name, p.post_name, e.phone_number, e.email, e.id_employee
                            FROM employees as e
                            join posts as p on p.id_post = e.id_post
                            join organisations as o on o.id_organisation = e.id_organisation";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var employee = new EmployeeDto
                {
                    Id = reader.GetInt32(5),
                    EmployeeName = reader.GetString(0), // Получаем значение столбца "employee_name"
                    Organisation = reader.GetString(1), // Получаем значение столбца "status_name"
                    Post = reader.GetString(2), // Получаем значение столбца "employee_description"
                    PhoneNumber = reader.GetString(3),
                    Email = reader.GetString(4)
                };

                employees.Add(employee);
            }
            return employees;
        }

        public IEnumerable<EmployeeIdDto> GetIdEmployees()
        {
            var employees = new List<EmployeeIdDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT e.id_employee, e.employees_name
                                    FROM employees as e";
            using var reader = command.ExecuteReader();


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
    }
}
