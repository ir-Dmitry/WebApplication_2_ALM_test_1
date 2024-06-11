using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
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
                .ToDictionary(x => x.index + 1, x => x.step);
        }

        public StepIdDto GetIdSteps(int stepId)
        {
            return _stepRepository.GetIdSteps(stepId);
        }

        // Метод для добавления нового проекта
        public void AddStep(Step step)
        {
            _stepRepository.AddStep(step);
        }

        // Метод для обновления существующего проекта
        public void UpdateStep(int stepId, Step step)
        {
            _stepRepository.UpdateStep(stepId, step);
        }

        // Метод для удаления проекта
        public void DeleteStep(int stepId)
        {
            _stepRepository.DeleteStep(stepId);
        }
    }
}
