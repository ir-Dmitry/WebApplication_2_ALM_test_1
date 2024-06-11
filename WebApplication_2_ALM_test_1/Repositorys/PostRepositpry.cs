using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class PostRepository
    {
        private readonly Database _database;

        public PostRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<PostIdDto> GetIdPost()
        {
            string query = @"SELECT id_post, post_name
                                    FROM posts";

            var posts = new List<PostIdDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
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
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }


        public IEnumerable<PostDto> GetAllPosts()
        {
            string query = @"SELECT p.id_post, p.post_name,  p.id_work_time, p.post_description, p.salary, p.allowance
                            FROM posts as p";

            var posts = new List<PostDto>();

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
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
                // Если проект с указанным идентификатором не найден, возвращаем null или бросаем исключение
                throw new Exception("Не удалось найти данные.");
            }
        }

        public void AddPost(Post post)
        {
            string query = @"INSERT INTO posts (p.post_name,  p.id_work_time, p.post_description, p.salary, p.allowance)
                                    VALUES (@PostName, @IdWorkTime, @PostDescription, @Salary, @Allowance)";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@IdWorkTime", post.IdWorkTime);
            command.Parameters.AddWithValue("@PostName", post.PostName);
            command.Parameters.AddWithValue("@PostDescription", post.PostDescription);
            command.Parameters.AddWithValue("@Salary", post.Salary);
            command.Parameters.AddWithValue("@Allowance", post.Allowance);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось добавить данные.");
            }
        }

        public void UpdatePost(int postId, Post post)
        {
            string query = @"UPDATE posts 
                                    SET post_name = @PostName, 
                                        id_work_time = @IdWorkTime, 
                                        post_description= @PostDescription,
                                        salary = @Salary,
                                        allowance = @Allowance
                                    WHERE id_post = @PostId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@PostId", postId);
            command.Parameters.AddWithValue("@PostName", post.PostName);
            command.Parameters.AddWithValue("@IdWorkTime", post.IdWorkTime);
            command.Parameters.AddWithValue("@PostDescription", post.PostDescription);
            command.Parameters.AddWithValue("@Salary", post.Salary);
            command.Parameters.AddWithValue("@Allowance", post.Allowance);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось обновить данные.");
            }
        }


        public void DeletePost(int postId)
        {
            string query = @"DELETE FROM posts WHERE id_post = @PostId";

            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@PostId", postId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                throw new Exception("Не удалось удалить данные.");
            }
        }
    }
}
