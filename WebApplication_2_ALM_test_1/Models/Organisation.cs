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
        public string? OrganisationDescription { get; set; }

        /// <summary>
        /// Номер телефона организации.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта организации.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// ИНН (индивидуальный налоговый номер) организации.
        /// </summary>
        public Int64? TIN { get; set; }

        /// <summary>
        /// Список сотрудников, работающих в этой организации.
        /// </summary>
        public List<Employee>? Employee { get; set; }
    }
}
