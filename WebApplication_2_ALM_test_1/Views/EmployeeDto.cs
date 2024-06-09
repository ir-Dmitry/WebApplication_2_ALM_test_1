using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO для представления информации о сотруднике.
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название организации.
        /// </summary>
        public string? Organisation { get; set; }

        /// <summary>
        /// Название должности.
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Номер телефона сотрудника.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта сотрудника.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Список задач сотрудника.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Models.Task>? Task{ get; set; }
    }

    /// <summary>
    /// DTO для представления идентификатора сотрудника и его имени.
    /// </summary>
    public class EmployeeIdDto
    {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список задач сотрудника.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Models.Task>? Task { get; set; }
    }
}
