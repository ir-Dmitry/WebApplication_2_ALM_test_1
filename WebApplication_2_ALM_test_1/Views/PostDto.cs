using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий должность.
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// Статус должности.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Рабочее время должности (в часах).
        /// </summary>
        public byte WorkTime { get; set; }

        /// <summary>
        /// Наименование должности.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание должности.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Заработная плата по должности.
        /// </summary>
        public int? Salary { get; set; }

        /// <summary>
        /// Дополнительные выплаты по должности.
        /// </summary>
        public byte? Allowance { get; set; }

        /// <summary>
        /// Список сотрудников на данной должности.
        /// </summary>
        public List<Employee>? Employee { get; set; }
    }

    /// <summary>
    /// DTO, представляющий идентификатор должности.
    /// </summary>
    public class PostIdDto
    {
        /// <summary>
        /// Идентификатор должности.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование должности.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список сотрудников на данной должности.
        /// </summary>
        public List<Employee>? Employee { get; set; }
    }

}
