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

        public Dictionary<int, OrganisationDto> GetAllOrganisations()
        {
            var organisations = _organisationRepository.GetAllOrganisations();
            return organisations
                .Select((organisation, index) => new { organisation, index })
                .ToDictionary(x => x.index + 1, x => x.organisation);
        }

        public IEnumerable<OrganisationIdDto> GetIdOrganisation()
        {
            return _organisationRepository.GetIdOrganisation();
        }

        // Метод для добавления нового проекта
        public void AddOrganisation(Organisation organisation)
        {
            _organisationRepository.AddOrganisation(organisation);
        }

        // Метод для обновления существующего проекта
        public void UpdateOrganisation(int organisationId, Organisation organisation)
        {
            _organisationRepository.UpdateOrganisation(organisationId, organisation);
        }

        // Метод для удаления проекта
        public void DeleteOrganisation(int organisationId)
        {
            _organisationRepository.DeleteOrganisation(organisationId);
        }
    }
}
