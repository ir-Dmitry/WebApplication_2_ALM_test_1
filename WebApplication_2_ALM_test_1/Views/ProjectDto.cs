﻿using System.Data.SqlTypes;
using WebApplication_2_ALM_test_1.Models;

namespace WebApplication_2_ALM_test_1.DTO
{
    /// <summary>
    /// DTO, представляющий проект.
    /// </summary>
    public class ProjectDto
    {
        /// <summary>
        /// Статус проекта.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Наименование проекта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание проекта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала проекта.
        /// </summary>
        public string? StartDate { get; set; }

        /// <summary>
        /// Дата окончания проекта.
        /// </summary>
        public string? EndDate { get; set; }

        /// <summary>
        /// Запланированный бюджет проекта.
        /// </summary>
        public string PlunedBudget { get; set; }
    }

    /// <summary>
    /// DTO, представляющий проект с идентификатором.
    /// </summary>
    public class ProjectIdDto
    {
        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование проекта.
        /// </summary>
        public string Name { get; set; }
    }
}
