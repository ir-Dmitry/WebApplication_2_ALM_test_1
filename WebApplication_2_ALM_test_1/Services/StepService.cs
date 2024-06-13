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
        private readonly StepRepository _stepRepository; // Поле для хранения репозитория этапов

        public StepService(StepRepository stepRepository)
        {
            _stepRepository = stepRepository; // Инициализация репозитория в конструкторе
        }

        // Метод для получения этапов по проекту и сотруднику
        public Dictionary<int, StepDto> GetStepsByProjectAndEmployee(int projectId, int? employeeId = null)
        {
            var steps = _stepRepository.GetStepsByProjectAndEmployee(projectId, employeeId); // Получение этапов из репозитория
            return steps
                .Select((step, index) => new { step, index }) // Преобразование каждого этапа в пару (этап, индекс)
                .ToDictionary(x => x.index + 1, x => x.step); // Преобразование в словарь, с индексацией с 1
        }

        // Метод для получения конкретного этапа по идентификатору
        public StepIdDto GetStepById(int stepId)
        {
            return _stepRepository.GetStepById(stepId); // Вызов метода репозитория для получения этапа по идентификатору
        }

        // Метод для добавления нового этапа
        public void AddStep(Step step)
        {
            string validationError = step.ValidateDates(); // Проверка валидации дат этапа
            if (validationError != null) // Если есть ошибка валидации
            {
                throw new ValidationException(validationError); // Бросаем исключение с сообщением об ошибке
            }
            _stepRepository.AddStep(step); // Вызов метода репозитория для добавления этапа
        }

        // Метод для обновления существующего этапа
        public void UpdateStep(int stepId, Step step)
        {
            string validationError = step.ValidateDates(); // Проверка валидации дат этапа
            if (validationError != null) // Если есть ошибка валидации
            {
                throw new ValidationException(validationError); // Бросаем исключение с сообщением об ошибке
            }
            _stepRepository.UpdateStep(stepId, step); // Вызов метода репозитория для обновления этапа
        }

        // Метод для удаления этапа
        public void DeleteStep(int stepId)
        {
            _stepRepository.DeleteStep(stepId); // Вызов метода репозитория для удаления этапа
        }
    }
}
