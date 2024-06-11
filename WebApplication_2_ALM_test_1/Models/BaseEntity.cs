using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace WebApplication_2_ALM_test_1.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор статуса, внешний ключ на сущность Status.
        /// </summary>
        [Required]
        public int IdStatus { get; set; }

        /// <summary>
        /// Связанный статус.
        /// </summary>
        
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public virtual Status? Status { get; set; }

        /// <summary>
        /// Название сущности.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Описание сущности.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Дата начала. Дата в формате [yyyy-MM-dd]
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Дата окончания. Дата в формате [yyyy-MM-dd]
        /// </summary>
        public string EndDate { get; set; }


        /// <summary>
        /// Метод для валидации дат.
        /// </summary>
        /// <returns>Сообщение об ошибке, если валидация не прошла, или null, если все корректно.</returns>
        public string ValidateDates()
        {
            DateTime startDateTime;
            DateTime endDateTime;

            // Проверка формата даты начала
            if (!DateTime.TryParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime))
            {
                return "Некорректный формат даты начала. Ожидается формат [yyyy-MM-dd].";
            }

            // Проверка формата даты окончания
            if (!DateTime.TryParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateTime))
            {
                return "Некорректный формат даты окончания. Ожидается формат [yyyy-MM-dd].";
            }

            // Проверка последовательности дат
            if (startDateTime > endDateTime)
            {
                return "Дата начала не может быть позже даты окончания.";
            }

            return null; // Все корректно
        }
    }
}
