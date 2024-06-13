using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository PostRepository)
        {
            _postRepository = PostRepository;
        }

        // Метод для получения всех должностей
        public Dictionary<int, PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAllPosts(); // Получение всех должностей через репозиторий
            return posts
                .Select((post, index) => new { post, index }) // Преобразование списка должностей в анонимные объекты с индексом
                .ToDictionary(x => x.index + 1, x => x.post); // Преобразование анонимных объектов в словарь, где ключ - индекс + 1, значение - должность
        }

        // Метод для получения идентификаторов должностей
        public IEnumerable<PostIdDto> GetIdPost()
        {
            return _postRepository.GetIdPost(); // Получение идентификаторов должностей через репозиторий
        }

        // Метод для добавления новой должности
        public void AddPost(Post post)
        {
            _postRepository.AddPost(post); // Добавление должности через репозиторий
        }

        // Метод для обновления данных существующей должности
        public void UpdatePost(int postId, Post post)
        {
            _postRepository.UpdatePost(postId, post); // Обновление данных должности через репозиторий
        }

        // Метод для удаления должности
        public void DeletePost(int postId)
        {
            _postRepository.DeletePost(postId); // Удаление должности через репозиторий
        }
    }
}
