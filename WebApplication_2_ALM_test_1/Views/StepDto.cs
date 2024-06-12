using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий шаг проекта.
    /// </summary>
    public class StepDto : BaseEntityDto
    {

        /// <summary>
        /// Статус шага проекта.
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Описание шага проекта.
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO, представляющий шаг проекта с идентификатором.
    /// </summary>
    public class StepIdDto : BaseEntityDto
    {
        /// <summary>
        /// Фактический бюджет.
        /// </summary>
        public string? TaskReward { get; set; }
        /// <summary>
        /// Сотрудники на этапе.
        /// </summary>
        public string? Employees { get; set; }
    }

}
