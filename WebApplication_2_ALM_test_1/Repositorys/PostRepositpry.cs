using System.Collections.Generic;
using WebApplication_2_ALM_test_1.DTO;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace WebApplication_2_ALM_test_1.Repository
{
    public class PostRepository
    {
        private readonly Database _database;

        public PostRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = new List<PostDto>();
            using var connection = _database.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT p.post_name, p.post_description, w.work_time, p.salary, p.allowance
                                  FROM posts AS p
                                  JOIN work_time AS w ON w.id_work_time= p.id_work_time";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var post = new PostDto
                {
                    Name = reader.GetString(0),
                    Description = reader.GetString(1),
                    WorkTime = reader.GetByte(2),
                    Salary = reader.GetInt32(3),
                    Allowance = reader.GetByte(4)

                };

                posts.Add(post);
            }
            return posts;
        }
    }
}
