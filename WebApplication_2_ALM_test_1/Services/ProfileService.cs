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

        public IEnumerable<ProfileDto> GetProfileByEmployeeId(int employeeId)
        {
            return _profileRepository.GetProfileByEmployeeId(employeeId);
        }
    }
}
