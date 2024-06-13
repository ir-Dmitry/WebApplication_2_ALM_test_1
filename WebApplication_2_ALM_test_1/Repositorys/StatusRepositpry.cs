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

        // Метод для получения идентификаторов и названий статусов
        public IEnumerable<StatusDto> GetIdStatus()
        {
            // Создаем список для хранения объектов DTO статусов
            var statuses = new List<StatusDto>();

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT s.id_status, s.status_name
                                    FROM _status AS s";

            // Выполняем запрос и получаем данные через SqlDataReader
            using var reader = command.ExecuteReader();

            // Перебираем результаты запроса и добавляем DTO статусов в список
            while (reader.Read())
            {
                var status = new StatusDto
                {
                    Id = reader.GetByte(0),         // Получаем значение первого столбца (id_status)
                    Name = reader.GetString(1)      // Получаем значение второго столбца (status_name)
                };

                statuses.Add(status);   // Добавляем созданный объект DTO в список
            }

            return statuses;    // Возвращаем список DTO статусов
        }
    }
}
