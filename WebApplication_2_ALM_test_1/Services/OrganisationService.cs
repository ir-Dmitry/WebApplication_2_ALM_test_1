using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class OrganisationService
    {
        private readonly OrganisationRepository _organisationRepository;

        public OrganisationService(OrganisationRepository OrganisationRepository)
        {
            _organisationRepository = OrganisationRepository;
        }

        // Метод для получения всех организаций
        public Dictionary<int, OrganisationDto> GetAllOrganisations()
        {
            var organisations = _organisationRepository.GetAllOrganisations(); // Получение всех организаций через репозиторий
            return organisations
                .Select((organisation, index) => new { organisation, index }) // Преобразование списка организаций в анонимные объекты с индексом
                .ToDictionary(x => x.index + 1, x => x.organisation); // Преобразование анонимных объектов в словарь, где ключ - индекс + 1, значение - организация
        }

        // Метод для получения идентификаторов организаций
        public IEnumerable<OrganisationIdDto> GetIdOrganisation()
        {
            return _organisationRepository.GetIdOrganisation(); // Получение идентификаторов организаций через репозиторий
        }

        // Метод для добавления новой организации
        public void AddOrganisation(Organisation organisation)
        {
            var validationError = organisation.Validate(); // Проверка валидации данных организации
            if (validationError != null)
            {
                throw new ArgumentException(validationError); // Если есть ошибки валидации, выбрасываем исключение
            }

            _organisationRepository.AddOrganisation(organisation); // Добавление организации через репозиторий
        }

        // Метод для обновления данных существующей организации
        public void UpdateOrganisation(int organisationId, Organisation organisation)
        {
            var validationError = organisation.Validate(); // Проверка валидации данных организации
            if (validationError != null)
            {
                throw new ArgumentException(validationError); // Если есть ошибки валидации, выбрасываем исключение
            }

            _organisationRepository.UpdateOrganisation(organisationId, organisation); // Обновление данных организации через репозиторий
        }

        // Метод для удаления организации
        public void DeleteOrganisation(int organisationId)
        {
            _organisationRepository.DeleteOrganisation(organisationId); // Удаление организации через репозиторий
        }
    }
}
