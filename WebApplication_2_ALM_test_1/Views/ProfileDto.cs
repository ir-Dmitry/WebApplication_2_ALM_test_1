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
        public string Post { get; set; }

        /// <summary>
        /// Телефонный номер сотрудника.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта сотрудника.
        /// </summary>
        public string? Email { get; set; }
    }

}
