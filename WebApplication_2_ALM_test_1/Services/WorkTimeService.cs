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
            _workTimeRepository = workTimeRepository;// Инициализация репозитория норм рабочего времени через конструктор
        }

        // Метод сервиса для получения идентификаторов и рабочего времени
        public IEnumerable<WorkTimeDto> GetIdWorkTime()
        {
            return _workTimeRepository.GetIdWorkTime(); // Вызов метода репозитория для получения данных о рабочем времени
        }
    }
}
