namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Задача, связанная с определенным шагом выполнения проекта.
    /// </summary>
    public class Task : BaseEntity
    {
        /// <summary>
        /// Идентификатор этапа, к которому относится эта задача.
        /// </summary>
        public int IdStep { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, назначенного на выполнение этой задачи.
        /// </summary>
        public int? IdEmployee { get; set; }

        /// <summary>
        /// Этап, к которому относится эта задача.
        /// </summary>
        public virtual Step Step { get; set; }

        /// <summary>
        /// Сотрудник, назначенный на выполнение этой задачи.
        /// </summary>
        public virtual Employee? Employee { get; set; }

        /// <summary>
        /// Продолжительность выполнения этой задачи (в часах).
        /// </summary>
        public byte? TaskTime { get; set; }
    }
}
