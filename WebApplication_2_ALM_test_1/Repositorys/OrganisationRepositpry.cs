using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class OrganisationRepository
    {
        // Поле для хранения ссылки на базу данных
        private readonly Database _database;

        // Конструктор для инициализации репозитория с базой данных
        public OrganisationRepository(Database database)
        {
            _database = database;
        }

        // Метод для получения списка организаций с их идентификаторами и названиями
        public IEnumerable<OrganisationIdDto> GetIdOrganisation()
        {
            // SQL-запрос для получения идентификаторов и названий организаций
            string query = @"SELECT id_organisation, organisation_name
                                    FROM organisations";

            // Создаем список для хранения результатов
            var organisations = new List<OrganisationIdDto>();

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
                    // Создаем новый объект OrganisationIdDto и добавляем его в список
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
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения всех данных об организациях
        public IEnumerable<OrganisationDto> GetAllOrganisations()
        {
            // SQL-запрос для получения полной информации об организациях
            string query = @"SELECT o.organisation_name,  o.phone_number, o.email, o.id_organisation
                            FROM organisations as o";

            // Создаем список для хранения результатов
            var organisations = new List<OrganisationDto>();

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
                    // Создаем новый объект OrganisationDto и добавляем его в список
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
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для добавления новой организации
        public void AddOrganisation(Organisation organisation)
        {
            // SQL-запрос для вставки новой организации
            string query = @"INSERT INTO organisations (organisation_name,  phone_number, email)
                                    VALUES (@OrganisationName, @PhoneNumber, @Email)";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@OrganisationName", organisation.OrganisationName);
            command.Parameters.AddWithValue("@PhoneNumber", organisation.PhoneNumber);
            command.Parameters.AddWithValue("@Email", organisation.Email);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось добавить данные, выбрасываем исключение
                throw new Exception("Не удалось добавить данные.");
            }
        }

        // Метод для обновления данных об организации
        public void UpdateOrganisation(int organisationId, Organisation organisation)
        {
            // SQL-запрос для обновления данных об организации
            string query = @"UPDATE organisations 
                                    SET organisation_name = @OrganisationName, 
                                        phone_number = @PhoneNumber, 
                                        email= @Email
                                    WHERE id_organisation = @OrganisationId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@OrganisationId", organisationId);
            command.Parameters.AddWithValue("@OrganisationName", organisation.OrganisationName);
            command.Parameters.AddWithValue("@PhoneNumber", organisation.PhoneNumber);
            command.Parameters.AddWithValue("@Email", organisation.Email);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось обновить данные, выбрасываем исключение
                throw new Exception("Не удалось обновить данные.");
            }
        }

        // Метод для удаления организации
        public void DeleteOrganisation(int organisationId)
        {
            // SQL-запрос для удаления организации
            string query = @"DELETE FROM organisations WHERE id_organisation = @OrganisationId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметр к запросу
            command.Parameters.AddWithValue("@OrganisationId", organisationId);

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

