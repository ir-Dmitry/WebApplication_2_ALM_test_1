using System.Collections.Generic;
using System.Linq;
using WebApplication_2_ALM_test_1.DTO;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;

namespace WebApplication_2_ALM_test_1.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _profileRepository;

        public ProfileService(ProfileRepository ProfileRepository)
        {
            _profileRepository = ProfileRepository;
        }

        // Метод для получения профиля по идентификатору сотрудника
        public ProfileDto GetProfileByEmployeeId(int employeeId)
        {
            return _profileRepository.GetProfileByEmployeeId(employeeId); // Получение профиля сотрудника по его идентификатору через репозиторий
        }

        // Метод для получения статуса профиля и задач
        public IEnumerable<TaskIdDto> GetProfileStatusTask()
        {
            return _profileRepository.GetProfileStatusTask(); // Получение статуса профиля и задач через репозиторий
        }

        // Метод для получения задач профиля сотрудника
        public Dictionary<int, ProfileTaskDto> GetProfileTask(int employeeId)
        {
            var projects = _profileRepository.GetProfileTask(employeeId); // Получение задач профиля сотрудника через репозиторий
            return projects
                .Select((project, index) => new { project, index }) // Преобразование списка задач в анонимные объекты с индексом
                .ToDictionary(x => x.index + 1, x => x.project); // Преобразование анонимных объектов в словарь, где ключ - индекс + 1, значение - задача профиля
        }

        // Метод для авторизации пользователя
        public Dictionary<string, object> Authorization(string password, string? phoneNumber = null, string? email = null)
        {
            var auth = new Authoraze(); // Создание объекта для работы с авторизацией
            string PasswordHash = auth.ComputeSha256Hash(password); // Хеширование пароля

            var (profile, admin) = _profileRepository.Authorization(PasswordHash, phoneNumber, email); // Выполнение авторизации через репозиторий
            return new Dictionary<string, object>
            {
                { "profile", profile }, // Возвращение профиля
                { "admin", admin } // Возвращение признака администратора
            };
        }

    }
}
