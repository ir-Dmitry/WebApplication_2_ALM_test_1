namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// Рабочее время, определяющее рабочие часы сотрудника в неделю.
    /// </summary>
    public class WorkTimeDto
    {
        /// <summary>
        /// Идентификатор рабочего времени.
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// Норма рабочего времени.
        /// </summary>
        public byte Name { get; set; }
    }
}
