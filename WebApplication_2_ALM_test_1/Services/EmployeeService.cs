using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _EmployeeRepository;

        public EmployeeService(EmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public Dictionary<int, EmployeeDto> GetAllEmployees()
        {
            var employees = _EmployeeRepository.GetAllEmployees();
            return employees
                .Select((employee, index) => new { employee, index })
                .ToDictionary(x => x.index, x => x.employee);
        }
    }
}
