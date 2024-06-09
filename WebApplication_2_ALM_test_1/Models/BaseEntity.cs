using System.ComponentModel.DataAnnotations;

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
        /// Дата начала.
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Дата окончания.
        /// </summary>
        public string EndDate { get; set; }
    }
}
