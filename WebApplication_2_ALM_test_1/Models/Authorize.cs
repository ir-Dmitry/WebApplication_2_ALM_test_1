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
    public class Authoraze
    {
        /// <summary>
        /// Вычисляет SHA-256 хэш для входных данных.
        /// </summary>
        /// <param name="rawData">Исходные данные для вычисления хэша.</param>
        /// <returns>Хэш в виде строки.</returns>
        public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Вычисляем хэш - возвращаем как строку
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Преобразуем каждый байт в строку шестнадцатеричного формата и добавляем к результату
                }
                return builder.ToString(); // Возвращаем строковое представление хэша
            }
        }

        /// <summary>
        /// Проверяет соответствие введенного значения хэшу SHA-256.
        /// </summary>
        /// <param name="input">Введенное значение для проверки.</param>
        /// <param name="hash">Сохраненный хэш для сравнения.</param>
        /// <returns>True, если введенное значение соответствует сохраненному хэшу; в противном случае - false.</returns>
        public bool VerifySha256Hash(string input, string hash)
        {
            // Вычисляем хэш введенного пароля
            string hashOfInput = ComputeSha256Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            // Сравниваем два хэша
            return comparer.Compare(hashOfInput, hash) == 0;
        }

        /// <summary>
        /// Генерирует случайный пароль заданной длины.
        /// </summary>
        /// <param name="length">Длина пароля (по умолчанию 12 символов).</param>
        /// <returns>Случайно сгенерированный пароль.</returns>
        public string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (0 < length--)
            {
                password.Append(validChars[random.Next(validChars.Length)]); // Добавляем случайный символ из допустимых символов
            }

            return password.ToString(); // Возвращаем сгенерированный пароль
        }
    }
}
