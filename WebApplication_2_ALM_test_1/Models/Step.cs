using System.Threading.Tasks;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// этап выполнения проекта.
    /// </summary>
    public class Step : BaseEntity
    {
        /// <summary>
        /// Идентификатор проекта, к которому относится этот шаг.
        /// </summary>
        public int IdPoject { get; set; }

        /// <summary>
        /// Проект, к которому относится этот этап.
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// Планируемый бюджет для этого этапа.
        /// </summary>
        public string? PlanedBudget { get; set; }

        /// <summary>
        /// Список задач, связанных с этим этапом.
        /// </summary>
        public List<Task>? Task { get; set; }
    }
}
