using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Dictionary<int, StepDto> GetStepsByProjectAndEmployee(int projectId, int? employeeId = null)
        {
            var steps = _stepRepository.GetStepsByProjectAndEmployee(projectId, employeeId);
            return steps
                .Select((step, index) => new { step, index })
                .ToDictionary(x => x.index + 1, x => x.step);
        }

        public StepIdDto GetStepById(int stepId)
        {
            return _stepRepository.GetStepById(stepId);
        }

        // Метод для добавления нового проекта
        public void AddStep(Step step)
        {
            string validationError = step.ValidateDates();
            if (validationError != null)
            {
                throw new ValidationException(validationError);
            }
            _stepRepository.AddStep(step);
        }

        // Метод для обновления существующего проекта
        public void UpdateStep(int stepId, Step step)
        {
            string validationError = step.ValidateDates();
            if (validationError != null)
            {
                throw new ValidationException(validationError);
            }
            _stepRepository.UpdateStep(stepId, step);
        }

        // Метод для удаления проекта
        public void DeleteStep(int stepId)
        {
            _stepRepository.DeleteStep(stepId);
        }
    }
}
