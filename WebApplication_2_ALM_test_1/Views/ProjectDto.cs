using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    public class ProjectDto
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string PlunedBudget { get; set; }

        // Другие свойства проекта, если необходимо
        public List<Step>? Step { get; set; }

    }
}
