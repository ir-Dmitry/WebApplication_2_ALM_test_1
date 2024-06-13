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

        // Метод для получения всех сотрудников
        public Dictionary<int, EmployeeDto> GetAllEmployee()
        {
            var employees = _employeeRepository.GetAllEmployee(); // Получение всех сотрудников через репозиторий
            return employees
                .Select((employee, index) => new { employee, index }) // Преобразование списка сотрудников в анонимные объекты с индексом
                .ToDictionary(x => x.index + 1, x => x.employee); // Преобразование анонимных объектов в словарь, где ключ - индекс + 1, значение - сотрудник
        }

        // Метод для получения идентификаторов сотрудников
        public IEnumerable<EmployeeIdDto> GetIdEmployee()
        {
            return _employeeRepository.GetIdEmployee(); // Получение идентификаторов сотрудников через репозиторий
        }

        // Метод для добавления нового сотрудника
        public string AddEmployee(Employee employee)
        {
            var validationError = employee.Validate(); // Проверка валидации данных сотрудника
            if (validationError != null)
            {
                throw new ArgumentException(validationError); // Если есть ошибки валидации, выбрасываем исключение
            }

            var auth = new Authoraze(); // Создание объекта для работы с авторизацией
            var password = auth.GenerateRandomPassword(); // Генерация случайного пароля
            var PasswordHash = auth.ComputeSha256Hash(password); // Хеширование пароля

            _employeeRepository.AddEmployee(employee, PasswordHash); // Добавление сотрудника через репозиторий
            return password; // Возвращаем сгенерированный пароль
        }

        // Метод для обновления данных существующего сотрудника
        public void UpdateEmployee(int employeeId, Employee employee)
        {
            var validationError = employee.Validate(); // Проверка валидации данных сотрудника
            if (validationError != null)
            {
                throw new ArgumentException(validationError); // Если есть ошибки валидации, выбрасываем исключение
            }
            _employeeRepository.UpdateEmployee(employeeId, employee); // Обновление данных сотрудника через репозиторий
        }

        // Метод для удаления сотрудника
        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId); // Удаление сотрудника через репозиторий
        }
    }
}
