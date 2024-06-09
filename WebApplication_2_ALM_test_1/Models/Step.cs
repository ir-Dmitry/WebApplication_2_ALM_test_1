using System.Text.Json.Serialization;
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
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public virtual Project Project { get; set; }

        /// <summary>
        /// Планируемый бюджет для этого этапа.
        /// </summary>
        public string? PlannedBudget { get; set; }

        /// <summary>
        /// Список задач, связанных с этим этапом.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Task>? Task { get; set; }
    }
}
