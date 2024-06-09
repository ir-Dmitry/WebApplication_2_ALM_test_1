using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий задачу.
    /// </summary>
    public class TaskDto
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Статус задачи.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Сотрудник, назначенный на задачу.
        /// </summary>
        public string Employee { get; set; }

        /// <summary>
        /// Наименование задачи.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала выполнения задачи.
        /// </summary>
        public string? StartDate { get; set; }

        /// <summary>
        /// Дата завершения выполнения задачи.
        /// </summary>
        public string? EndDate { get; set; }

        /// <summary>
        /// Вознаграждение за выполнение задачи.
        /// </summary>
        public int? Reward { get; set; }
    }

    /// <summary>
    /// DTO, представляющий задачу с идентификатором.
    /// </summary>
    public class TaskIdDto
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование задачи.
        /// </summary>
        public string Name { get; set; }
    }

}
