using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class StatusService
    {
        private readonly StatusRepository _statusRepository;

        public StatusService(StatusRepository statusRepository)
        {
            _statusRepository = statusRepository; // Инициализация репозитория статусов через конструктор
        }

        // Метод для получения идентификаторов статусов
        public IEnumerable<StatusDto> GetIdStatus()
        {
            return _statusRepository.GetIdStatus(); // Вызов метода репозитория для получения идентификаторов статусов
        }
    }
}
