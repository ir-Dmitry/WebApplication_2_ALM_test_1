using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class PostRepository
    {
        // Поле для хранения ссылки на базу данных
        private readonly Database _database;

        // Конструктор для инициализации репозитория с базой данных
        public PostRepository(Database database)
        {
            _database = database;
        }

        // Метод для получения списка должностей с их идентификаторами и названиями
        public IEnumerable<PostIdDto> GetIdPost()
        {
            // SQL-запрос для получения идентификаторов и названий должностей
            string query = @"SELECT id_post, post_name
                                    FROM posts";

            // Создаем список для хранения результатов
            var posts = new List<PostIdDto>();

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
                    // Создаем новый объект PostIdDto и добавляем его в список
                    var post = new PostIdDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    posts.Add(post);
                }
                return posts;
            }
            else
            {
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для получения всех данных о должностях
        public IEnumerable<PostDto> GetAllPosts()
        {
            // SQL-запрос для получения полной информации о должностях
            string query = @"SELECT p.id_post, p.post_name,  p.id_work_time, p.post_description, p.salary, p.allowance
                            FROM posts as p";

            // Создаем список для хранения результатов
            var posts = new List<PostDto>();

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
                    // Создаем новый объект PostDto и добавляем его в список
                    var post = new PostDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        WorkTime = reader.GetByte(2),
                        Description = reader.GetString(3),
                        Salary = reader.GetInt32(4),
                        Allowance = reader.GetByte(5)
                    };

                    posts.Add(post);
                }
                return posts;
            }
            else
            {
                // Если данные не найдены, выбрасываем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        // Метод для добавления новой должности
        public void AddPost(Post post)
        {
            // SQL-запрос для вставки новой должности
            string query = @"INSERT INTO posts (p.post_name,  p.id_work_time, p.post_description, p.salary, p.allowance)
                                    VALUES (@PostName, @IdWorkTime, @PostDescription, @Salary, @Allowance)";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@PostName", post.PostName);
            command.Parameters.AddWithValue("@IdWorkTime", post.IdWorkTime);
            command.Parameters.AddWithValue("@PostDescription", post.PostDescription);
            command.Parameters.AddWithValue("@Salary", post.Salary);
            command.Parameters.AddWithValue("@Allowance", post.Allowance);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось добавить данные, выбрасываем исключение
                throw new Exception("Не удалось добавить данные.");
            }
        }

        // Метод для обновления данных о должности
        public void UpdatePost(int postId, Post post)
        {
            // SQL-запрос для обновления данных о должности
            string query = @"UPDATE posts 
                                    SET post_name = @PostName, 
                                        id_work_time = @IdWorkTime, 
                                        post_description= @PostDescription,
                                        salary = @Salary,
                                        allowance = @Allowance
                                    WHERE id_post = @PostId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметры к запросу
            command.Parameters.AddWithValue("@PostId", postId);
            command.Parameters.AddWithValue("@PostName", post.PostName);
            command.Parameters.AddWithValue("@IdWorkTime", post.IdWorkTime);
            command.Parameters.AddWithValue("@PostDescription", post.PostDescription);
            command.Parameters.AddWithValue("@Salary", post.Salary);
            command.Parameters.AddWithValue("@Allowance", post.Allowance);

            // Выполняем запрос и проверяем количество затронутых строк
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                // Если не удалось обновить данные, выбрасываем исключение
                throw new Exception("Не удалось обновить данные.");
            }
        }

        // Метод для удаления должности
        public void DeletePost(int postId)
        {
            // SQL-запрос для удаления должности
            string query = @"DELETE FROM posts WHERE id_post = @PostId";

            // Открываем соединение с базой данных
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            // Добавляем параметр к запросу
            command.Parameters.AddWithValue("@PostId", postId);

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

