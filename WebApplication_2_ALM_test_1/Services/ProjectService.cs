using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class ProjectService
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Dictionary<int, ProjectDto> GetAllProjects()
        {
            var projects = _projectRepository.GetAllProjects();
            return projects
                .Select((project, index) => new { project, index })
                .ToDictionary(x => x.index, x => x.project);
        }

        public IEnumerable<ProjectIdDto> GetIdProjects()
        {
            return _projectRepository.GetIdProjects();
        }
    }
}
