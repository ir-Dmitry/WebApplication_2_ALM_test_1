﻿using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Рабочее время, определяющее рабочие часы сотрудника в неделю.
    /// </summary>
    public class WorkTime
    {
        /// <summary>
        /// Идентификатор рабочего времени.
        /// </summary>
        public byte IdWorkTime { get; set; }

        /// <summary>
        /// Норма рабочего времени.
        /// </summary>
        public byte _WorkTime { get; set; }

        /// <summary>
        /// Список должностей, связанных с этим рабочим временем.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Post>? Post { get; set; }
    }

}
