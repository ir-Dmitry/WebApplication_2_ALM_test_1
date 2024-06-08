using Microsoft.Extensions.Hosting;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Сотрудник организации.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public int IdEmployee { get; set; }

        /// <summary>
        /// Идентификатор организации, к которой принадлежит сотрудник.
        /// </summary>
        public int? IdOrganisation { get; set; }

        /// <summary>
        /// Идентификатор должности сотрудника.
        /// </summary>
        public int IdPost { get; set; }

        /// <summary>
        /// Организация, к которой принадлежит сотрудник.
        /// </summary>
        public virtual Organisation? Organisations { get; set; }

        /// <summary>
        /// Должность сотрудника.
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Номер телефона сотрудника.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты сотрудника.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Список задач, назначенных этому сотруднику.
        /// </summary>
        public List<Task>? Task { get; set; }
    }
}
