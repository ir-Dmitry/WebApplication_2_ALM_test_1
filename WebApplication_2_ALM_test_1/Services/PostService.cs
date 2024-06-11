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

        public Dictionary<int, PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAllPosts();
            return posts
                .Select((post, index) => new { post, index })
                .ToDictionary(x => x.index + 1, x => x.post);
        }

        public IEnumerable<PostIdDto> GetIdPost()
        {
            return _postRepository.GetIdPost();
        }

        // Метод для добавления нового проекта
        public void AddPost(Post post)
        {
            _postRepository.AddPost(post);
        }

        // Метод для обновления существующего проекта
        public void UpdatePost(int postId, Post post)
        {
            _postRepository.UpdatePost(postId, post);
        }

        // Метод для удаления проекта
        public void DeletePost(int postId)
        {
            _postRepository.DeletePost(postId);
        }
    }
}
