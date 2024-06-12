using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий проект.
    /// </summary>
    public class BaseEntityDto
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
    /// Дата начала проекта.
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// Дата окончания проекта.
    /// </summary>
    public string? EndDate { get; set; }

    /// <summary>
    /// Запланированный бюджет проекта.
    /// </summary>
    public string? PlanedBudget { get; set; }
    }
}
