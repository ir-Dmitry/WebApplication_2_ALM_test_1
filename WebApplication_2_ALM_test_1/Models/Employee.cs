using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication_2_ALM_test_1.Models
{
    /// <summary>
    /// Сотрудник организации.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public int IdEmployee { get; set; }

        /// <summary>
        /// Идентификатор организации, к которой принадлежит сотрудник.
        /// </summary>
        public int? IdOrganisation { get; set; }

        /// <summary>
        /// Идентификатор должности сотрудника.
        /// </summary>
        public int? IdPost { get; set; }

        /// <summary>
        /// Организация, к которой принадлежит сотрудник.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public virtual Organisation? Organisations { get; set; }

        /// <summary>
        /// Должность сотрудника.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public virtual Post? Post { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Номер телефона сотрудника. Ожидается международный формат.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты сотрудника. Почта в формате example@examp.ex
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Пароль сотрудника. Генерируется автоматически
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// Список задач, назначенных этому сотруднику.
        /// </summary>
        [JsonIgnore] // Игнорируем свойства, которые создают избыточную вложенность.
        public List<Task>? Task { get; set; }

        public string? Validate()
        {
            // Пример кастомной валидации для телефона (можно добавить регулярное выражение)
            if (!string.IsNullOrEmpty(PhoneNumber) && !Regex.IsMatch(PhoneNumber, @"^\+?[1-9]\d{6,14}$"))
            {
                return "Некорректный номер телефона. Ожидается международный формат.";
            }

            // Пример кастомной валидации для email (если требуется специфическая проверка)
            if (!string.IsNullOrEmpty(Email) && !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "Некорректный формат электронной почты. Почта в формате example@examp.ex";
            }

            return null;
        }
    }
}
