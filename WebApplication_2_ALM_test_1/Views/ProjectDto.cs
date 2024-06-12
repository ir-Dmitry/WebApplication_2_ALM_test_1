using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий проект.
    /// </summary>
public class ProjectDto : BaseEntityDto
    {
        /// <summary>
        /// Статус проекта.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Описание проекта.
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO, представляющий проект с идентификатором и числом задач.
    /// </summary>
    public class ProjectsIdDto
    {
        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование проекта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество задач в проекте.
        /// </summary>
        public int? Amount { get; set; }
    }

    /// <summary>
    /// DTO, представляющий проект с идентификатором и числом задач.
    /// </summary>
    public class ProjectIdDto : BaseEntityDto
    {
        /// <summary>
        /// Фактический бюджет.
        /// </summary>
        public string? TaskReward { get; set; }
        /// <summary>
        /// Сотрудники на проекте.
        /// </summary>
        public string? Employees { get; set; }
    }
}
