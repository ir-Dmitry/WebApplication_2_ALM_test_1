using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
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

        public Dictionary<string, object> Authorization(string password, string? phoneNumber = null, string? email = null)
        {
            var auth = new Authoraze();
            string PasswordHash = auth.ComputeSha256Hash(password);

            var (profile, admin) = _profileRepository.Authorization(PasswordHash, phoneNumber, email);
            return new Dictionary<string, object>
            {
                { "profile", profile },
                { "admin", admin }
            };
        }

    }
}
