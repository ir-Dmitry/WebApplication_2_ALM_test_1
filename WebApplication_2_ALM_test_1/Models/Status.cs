using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Статус проекта, этапа или задачи.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Идентификатор статуса.
        /// </summary>
        public byte IdStatus { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Список проектов, связанных с этим статусом.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Project>? Project { get; set; }

        /// <summary>
        /// Список этапов, связанных с этим статусом.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Step>? Step { get; set; }

        /// <summary>
        /// Список задач, связанных с этим статусом.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Task>? Task { get; set; }
    }

}
