﻿using System.Text.Json.Serialization;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Модель данных для проектов.
    /// </summary>
    public class Project : BaseEntity
    {
        /// <summary>
        /// Запланированный бюджет проекта. Точность до двух знаков после запятой.
        /// </summary>
        public decimal? PlannedBudget { get; set; }

        /// <summary>
        /// Список этапов, связанных с этим проектом.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Step>? Step { get; set; }
    }
}
