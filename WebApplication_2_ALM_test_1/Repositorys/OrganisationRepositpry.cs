using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class OrganisationRepository
    {
        private readonly Database _database;

        public OrganisationRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<OrganisationDto> GetAllOrganisations()
        {
            var organisations = new List<OrganisationDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT o.organisation_name,  o.phone_number, o.email, o.id_organisation
                                    FROM organisations as o";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var organisation = new OrganisationDto
                {
                    Id = reader.GetInt32(6),
                    Name = reader.GetString(0), // Получаем значение столбца "organisation_name"
                    PhoneNumber = reader.GetString(1),
                    Email = reader.GetString(2)
                };

                organisations.Add(organisation);
            }
            return organisations;
        }
    }
}
