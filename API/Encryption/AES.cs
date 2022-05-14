using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Encryption
{
    // source: https://www.cyberforum.ru/csharp-beginners/thread1370172.html#post7200863
    // I convert code from source into AES.
    static class AESHelper
    {
        // source: https://www.javainuse.com/aesgenerator
        // Cipher Block Chaining (CBC) - вектор инициализации (IV), чтобы сделать каждое сообщение уникальным.
        // Electronic Code Book (ECB) - не требует IV для шифрования.

        // Вектор инициализации. (128 bit)
        private static byte[] IV = { 68, 113, 118, 86, 108, 82, 69, 54, 70, 121, 114, 69, 51, 78, 52, 48 };
        // Секретный ключ. (optional: 128, 256 bit)
        private static byte[] Key = { 119, 121, 79, 51, 95, 100, 77, 113, 79, 106, 65, 89, 52, 88, 50, 65, 87, 107, 42, 50, 122, 52, 85, 49, 59, 115, 116, 49, 78, 99, 97, 70 };

        /// <summary>
        /// Генерирует новые "ключи" - IV и Key.
        /// </summary>
        public static void GenerateIvAndKey(string sIV = null, string sKey = null)
        {
            // Для работы AES требуется вектор инициализации (IV) и ключ (Key)
            // Операции шифрования/деширования должны использовать одинаковые значения IV и Key

            if (sIV != null && (sIV == "" || sIV.Length != 16))
            {
                throw new Exception("Размер вектора инициализации задан неверно!");
            }
            if (sKey != null && (sKey == "" || sKey.Length != 32))
            {
                throw new Exception("Размер ключа задан неверно!");
            }
            using (Aes aes = Aes.Create())
            {
                if (sIV == null)
                {
                    aes.GenerateIV();
                    AESHelper.IV = aes.IV;
                }
                else
                {
                    AESHelper.IV = Encoding.UTF8.GetBytes(sIV);
                }
                if (sKey == null)
                {
                    aes.GenerateKey();
                    AESHelper.Key = aes.Key;
                }
                else
                {
                    AESHelper.Key = Encoding.UTF8.GetBytes(sKey);
                }
            }

        }

        /// <summary>
        /// Создает копию файла по указанному пути, шифрует её и сохраняет по заданному пути.
        /// </summary>
        /// <param name="inputFile">Путь к исходному файлу</param>
        /// <param name="outputFile">Путь для сохранения шифрованного файла</param>
        public static void EncryptFile(string inputFile, string outputFile)
        {
            using (Aes tdes = Aes.Create())
            {

                // Для работы TripleDES требуется вектор инициализации (IV) и ключ (Key)
                // Операции шифрования/деширования должны использовать одинаковые значения IV и Key
                tdes.IV = AESHelper.IV;
                tdes.Key = AESHelper.Key;
                using (var inputStream = File.OpenRead(inputFile))
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                using (var encStream = new CryptoStream(outputStream, tdes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    outputStream.SetLength(0);
                    inputStream.CopyTo(encStream);
                }
            }
        }

        /// <summary>
        /// Создает копию шифрованного файла по указанному пути, ДЕшифрует её и сохраняет по заданному пути.
        /// </summary>
        /// <param name="inputFile">Путь к исходному файлу</param>
        /// <param name="outputFile">Путь для сохранения шифрованного файла</param>
        public static void DecryptFile(string inputFile, string outputFile)
        {
            using (Aes tdes = Aes.Create())
            {
                tdes.IV = AESHelper.IV;
                tdes.Key = AESHelper.Key;
                using (var inputStream = File.OpenRead(inputFile))
                using (var decStream = new CryptoStream(inputStream, tdes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    decStream.CopyTo(outputStream);
                }
            }
        }
        /// <summary>
        /// Шифрует заданную строку.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncryptString(string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            using (Aes tdes = Aes.Create())
            {
                tdes.IV = AESHelper.IV;
                tdes.Key = AESHelper.Key;

                byte[] byteBuff = Encoding.UTF8.GetBytes(source);
                // Используем ЕНкриптор.
                byte[] ecryptedByteBuff =
                    tdes.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length);
                return Convert.ToBase64String(ecryptedByteBuff);
            }
        }
        /// <summary>
        /// Расшифровывает заданную строку.
        /// </summary>
        /// <param name="encryptedString">Зашифрованная строка</param>
        /// <returns></returns>
        public static string DecryptString(string encryptedString)
        {
            if (string.IsNullOrEmpty(encryptedString)) return encryptedString;
            using (Aes aes = Aes.Create())
            {
                aes.IV = AESHelper.IV;
                aes.Key = AESHelper.Key;

                byte[] encryptedByteBuff = Convert.FromBase64String(encryptedString);

                // Испльзуем ДЕкриптор.
                byte[] byteBuff = aes.CreateDecryptor().TransformFinalBlock(
                    encryptedByteBuff, 0, encryptedByteBuff.Length);
                return Encoding.UTF8.GetString(byteBuff);
            }
        }

        /// <summary>
        /// Дешифрует указанный файл и возвращает его содержимое в виде строки.
        /// </summary>
        /// <param name="inputFile">Путь к зашифрованному файлу</param>
        public static string DecryptFileToString(string inputFile)
        {
            using (Aes aes = Aes.Create())
            {
                aes.IV = AESHelper.IV;
                aes.Key = AESHelper.Key;
                using (var inputStream = File.OpenRead(inputFile))
                using (var decStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    // convert stream to string
                    StreamReader reader = new StreamReader(decStream);
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
