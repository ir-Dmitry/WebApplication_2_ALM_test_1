using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class OrganisationService
    {
        private readonly OrganisationRepository _OrganisationRepository;

        public OrganisationService(OrganisationRepository OrganisationRepository)
        {
            _OrganisationRepository = OrganisationRepository;
        }

        public Dictionary<int, OrganisationDto> GetAllOrganisations()
        {
            var organisations = _OrganisationRepository.GetAllOrganisations();
            return organisations
                .Select((organisation, index) => new { organisation, index })
                .ToDictionary(x => x.index, x => x.organisation);
        }
    }
}
