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

        public IEnumerable<ProfileDto> GetProfileByEmployeeId(int employeeId)
        {
            var profiles = new List<ProfileDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT e.employees_name, p.post_name, e.phone_number, e.email
                            FROM employees as e
                            join posts as p on p.id_post = e.id_post
                            where e.id_employee = @EmployeeId";

            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var profile = new ProfileDto
                {
                    EmployeeName = reader.GetString(0), // Получаем значение столбца "profile_name"
                    Post = reader.GetString(1), // Получаем значение столбца "profile_description"
                    PhoneNumber = reader.GetString(2),
                    Email = reader.GetString(3)
                };

                profiles.Add(profile);
            }
            return profiles;
        }
    }
}
