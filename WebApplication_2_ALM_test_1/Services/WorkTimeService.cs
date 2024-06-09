using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class WorkTimeService
    {
        private readonly WorkTimeRepository _workTimeRepository;

        public WorkTimeService(WorkTimeRepository workTimeRepository)
        {
            _workTimeRepository = workTimeRepository;
        }

        public IEnumerable<WorkTimeDto> GetIdWorkTime()
        {
            return _workTimeRepository.GetIdWorkTime();
        }
    }
}
