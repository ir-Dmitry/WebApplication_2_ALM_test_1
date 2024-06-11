using System.Text.Json.Serialization;
using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий профиль сотрудника.
    /// </summary>
    public class ProfileDto
    {
        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Должность сотрудника.
        /// </summary>
        public string? Post { get; set; }

        /// <summary>
        /// Телефонный номер сотрудника.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта сотрудника.
        /// </summary>
        public string? Email { get; set; }
        public List<Step>? Step { get; set; }

    }

    /// <summary>
    /// DTO, представляющий Id сотрудника и его номер телефона.
    /// </summary>
    public class ProfileIdDto
    {
        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Телефонный номер сотрудника.
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
    /// <summary>
    /// DTO, представляющий задачу с идентификатором.
    /// </summary>
    public class ProfileTaskDto
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор проекта над задачей.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Наименование задачи.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Статус задачи.
        /// </summary>
        public string? Status { get; set; }
    }
}
