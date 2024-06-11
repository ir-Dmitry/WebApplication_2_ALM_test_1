using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository EmployeeRepository)
        {
            _employeeRepository = EmployeeRepository;
        }
        public Dictionary<int, EmployeeDto> GetAllEmployee()
        {
            var employees = _employeeRepository.GetAllEmployee();
            return employees
                .Select((employee, index) => new { employee, index })
                .ToDictionary(x => x.index + 1, x => x.employee);
        }

        public IEnumerable<EmployeeIdDto> GetIdEmployee()
        {
            return _employeeRepository.GetIdEmployee();
        }

        // Метод для добавления нового проекта
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }

        // Метод для обновления существующего проекта
        public void UpdateEmployee(int employeeId, Employee employee)
        {
            _employeeRepository.UpdateEmployee(employeeId, employee);
        }

        // Метод для удаления проекта
        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
        }
    }
}
