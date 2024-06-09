using System.Text.Json.Serialization;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Должность в организации.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Идентификатор должности.
        /// </summary>
        public int IdPost { get; set; }

        /// <summary>
        /// Идентификатор рабочего времени, связанного с этой должностью.
        /// </summary>
        public int? IdWorkTime { get; set; }

        /// <summary>
        /// Рабочее время, связанное с этой должностью.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public virtual WorkTime? WorkTime { get; set; }

        /// <summary>
        /// Название должности.
        /// </summary>
        public string PostName { get; set; }

        /// <summary>
        /// Описание должности.
        /// </summary>
        public string? PostDescription { get; set; }

        /// <summary>
        /// Заработная плата для этой должности.
        /// </summary>
        public int? Salary { get; set; }

        /// <summary>
        /// Надбавка к заработной плате в процентах.
        /// </summary>
        public byte? Allowance { get; set; }

        /// <summary>
        /// Список сотрудников, занимающих эту должность.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Employee>? Employee { get; set; }
    }
}
