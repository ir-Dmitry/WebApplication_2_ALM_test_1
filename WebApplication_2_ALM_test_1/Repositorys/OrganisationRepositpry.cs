using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class OrganisationRepository
    {
        private readonly Database _database;

        public OrganisationRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<OrganisationIdDto> GetIdOrganisation()
        {
            string query = @"SELECT id_organisation, organisation_name
                                    FROM organisations";

            var organisations = new List<OrganisationIdDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var organisation = new OrganisationIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    organisations.Add(organisation);
                }
                return organisations;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }


        public IEnumerable<OrganisationDto> GetAllOrganisations()
        {
            string query = @"SELECT o.organisation_name,  o.phone_number, o.email, o.id_organisation
                            FROM organisations as o";

            var organisations = new List<OrganisationDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    var organisation = new OrganisationDto
                    {
                        Id = reader.GetInt32(3),
                        Name = reader.GetString(0),
                        PhoneNumber = reader.GetString(1),
                        Email = reader.GetString(2)
                    };

                    organisations.Add(organisation);
                }
                return organisations;
            }
            else
            {
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public void AddOrganisation(Organisation organisation)
        {
            string query = @"INSERT INTO organisations (organisation_name,  phone_number, email)
                                    VALUES (@OrganisationName, @PhoneNumber, @Email)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@OrganisationName", organisation.OrganisationName);
            command.Parameters.AddWithValue("@PhoneNumber", organisation.PhoneNumber);
            command.Parameters.AddWithValue("@Email", organisation.Email);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные.");
            }
        }

        public void UpdateOrganisation(int organisationId, Organisation organisation)
        {
            string query = @"UPDATE organisations 
                                    SET organisation_name = @OrganisationName, 
                                        phone_number = @PhoneNumber, 
                                        email= @Email
                                    WHERE id_organisation = @OrganisationId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@OrganisationId", organisationId);
            command.Parameters.AddWithValue("@OrganisationName", organisation.OrganisationName);
            command.Parameters.AddWithValue("@PhoneNumber", organisation.PhoneNumber);
            command.Parameters.AddWithValue("@Email", organisation.Email);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные.");
            }
        }


        public void DeleteOrganisation(int organisationId)
        {
            string query = @"DELETE FROM organisations WHERE id_organisation = @OrganisationId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@OrganisationId", organisationId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
