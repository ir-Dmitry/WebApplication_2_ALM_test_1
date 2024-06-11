using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _profileRepository;

        public ProfileService(ProfileRepository ProfileRepository)
        {
            _profileRepository = ProfileRepository;
        }

        public ProfileDto GetProfileByEmployeeId(int employeeId)
        {
            return _profileRepository.GetProfileByEmployeeId(employeeId);
        }

        public IEnumerable<TaskIdDto> GetProfileStatusTask()
        {
            return _profileRepository.GetProfileStatusTask();
        }

        public Dictionary<int, ProfileTaskDto> GetProfileTask(int employeeId)
        {
            var projects = _profileRepository.GetProfileTask(employeeId);
            return projects
                .Select((project, index) => new { project, index })
                .ToDictionary(x => x.index + 1, x => x.project);
        }

        public int GetProfile(string phoneNumber, int employeeId)
        {
            return _profileRepository.GetProfile(phoneNumber, employeeId);
        }
    }
}
