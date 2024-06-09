using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class PostService
    {
        private readonly PostRepository _PostRepository;

        public PostService(PostRepository PostRepository)
        {
            _PostRepository = PostRepository;
        }

        public Dictionary<int, PostDto> GetAllPosts()
        {
            var posts = _PostRepository.GetAllPosts();
            return posts
                .Select((post, index) => new { post, index })
                .ToDictionary(x => x.index, x => x.post);
        }
    }
}
