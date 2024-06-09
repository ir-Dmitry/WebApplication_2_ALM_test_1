using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class WorkTimeRepository
    {
        private readonly Database _database;

        public WorkTimeRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<WorkTimeDto> GetIdWorkTime()
        {
            var workTimes = new List<WorkTimeDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT wt.id_work_time, wt.work_time
                                    FROM work_time AS wt";

            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var workTime = new WorkTimeDto
                {
                    Id = reader.GetByte(0),
                    Name = reader.GetByte(1)
                };

                workTimes.Add(workTime);
            }
            return workTimes;
        }
    }
}
