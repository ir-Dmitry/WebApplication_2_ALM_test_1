using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий шаг проекта.
    /// </summary>
    public class StepDto
    {
        /// <summary>
        /// Статус шага проекта.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Наименование шага проекта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание шага проекта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала шага проекта.
        /// </summary>
        public string? StartDate { get; set; }

        /// <summary>
        /// Дата завершения шага проекта.
        /// </summary>
        public string? EndDate { get; set; }

        /// <summary>
        /// Планируемый бюджет для шага проекта.
        /// </summary>
        public string PlunedBudget { get; set; }
    }

    /// <summary>
    /// DTO, представляющий шаг проекта с идентификатором.
    /// </summary>
    public class StepIdDto
    {
        /// <summary>
        /// Идентификатор шага проекта.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование шага проекта.
        /// </summary>
        public string Name { get; set; }
    }

}
