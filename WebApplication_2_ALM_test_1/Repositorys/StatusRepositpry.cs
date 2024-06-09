using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class StatusRepository
    {
        private readonly Database _database;

        public StatusRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<StatusDto> GetIdStatus()
        {
            var statuss = new List<StatusDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT s.id_status, s.status_name
                                    FROM _status AS s";

            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var status = new StatusDto
                {
                    Id = reader.GetByte(0),
                    Name = reader.GetString(1)
                };

                statuss.Add(status);
            }
            return statuss;
        }
    }
}
