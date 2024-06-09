namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// Статус проекта, этапа или задачи.
    /// </summary>
    public class StatusDto
    {
        /// <summary>
        /// Идентификатор статуса.
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        public string Name { get; set; }
    }

}
