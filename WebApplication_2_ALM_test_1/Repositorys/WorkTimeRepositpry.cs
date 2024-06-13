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

        // Метод для получения идентификаторов и рабочего времени
        public IEnumerable<WorkTimeDto> GetIdWorkTime()
        {
            var workTimes = new List<WorkTimeDto>(); // Создание списка для хранения данных о рабочем времени

            using var connection = _database.CreateConnection(); // Создание подключения к базе данных
            using var command = connection.CreateCommand(); // Создание команды для выполнения запроса
            command.CommandText = @"SELECT wt.id_work_time, wt.work_time
                                    FROM work_time AS wt"; // Установка текста запроса

            using var reader = command.ExecuteReader(); // Выполнение запроса и получение результатов

            while (reader.Read()) // Чтение данных из результата запроса
            {
                var workTime = new WorkTimeDto // Создание объекта WorkTimeDto для хранения данных о рабочем времени
                {
                    Id = reader.GetByte(0), // Получение идентификатора рабочего времени из первого столбца
                    Name = reader.GetByte(1) // Получение рабочего времени из второго столбца
                };

                workTimes.Add(workTime); // Добавление данных о рабочем времени в список
            }
            return workTimes; // Возврат списка рабочего времени
        }
    }
}
