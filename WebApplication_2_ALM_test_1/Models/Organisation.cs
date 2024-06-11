using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Организация.
    /// </summary>
    public class Organisation
    {
        /// <summary>
        /// Идентификатор организации.
        /// </summary>
        public int IdOrganisation { get; set; }

        /// <summary>
        /// Название организации.
        /// </summary>
        public string OrganisationName { get; set; }

        /// <summary>
        /// Описание организации.
        /// </summary>
        //public string? OrganisationDescription { get; set; }

        /// <summary>
        /// Номер телефона организации. Ожидается международный формат.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта организации. Почта в формате example@examp.ex
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// ИНН (индивидуальный налоговый номер) организации.
        /// </summary>
        //public Int64? TIN { get; set; }

        /// <summary>
        /// Список сотрудников, работающих в этой организации.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Employee>? Employee { get; set; }

        /// <summary>
        /// Метод для кастомной валидации.
        /// </summary>
        /// <returns>Сообщение об ошибке или null, если ошибок нет</returns>
        public string? Validate()
        {
            // Пример кастомной валидации для телефона (можно добавить регулярное выражение)
            if (!string.IsNullOrEmpty(PhoneNumber) && !Regex.IsMatch(PhoneNumber, @"^\+?[1-9]\d{6,14}$"))
            {
                return "Некорректный номер телефона. Ожидается международный формат.";
            }

            // Пример кастомной валидации для email (если требуется специфическая проверка)
            if (!string.IsNullOrEmpty(Email) && !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "Некорректный формат электронной почты. Почта в формате example@examp.ex";
            }

            return null;
        }
    }
}
