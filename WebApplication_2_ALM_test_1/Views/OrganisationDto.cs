using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий организацию.
    /// </summary>
    public class OrganisationDto
    {
        /// <summary>
        /// Наименование организации.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефонный номер организации.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта организации.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Список сотрудников организации.
        /// </summary>
        public List<Employee>? Employee { get; set; }
    }

    /// <summary>
    /// DTO, представляющий организацию с идентификатором.
    /// </summary>
    public class OrganisationIdDto
    {
        /// <summary>
        /// Идентификатор организации.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование организации.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список сотрудников организации.
        /// </summary>
        public List<Employee>? Employee { get; set; }
    }

}
