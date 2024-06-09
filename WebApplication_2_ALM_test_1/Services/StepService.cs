using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class StepService
    {
        private readonly StepRepository _stepRepository;

        public StepService(StepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        public Dictionary<int, StepDto> GetAllSteps()
        {
            var steps = _stepRepository.GetAllSteps();
            return steps
                .Select((step, index) => new { step, index })
                .ToDictionary(x => x.index, x => x.step);
        }

        public IEnumerable<StepIdDto> GetIdSteps(int projectId)
        {
            return _stepRepository.GetIdSteps(projectId);
        }
    }
}
