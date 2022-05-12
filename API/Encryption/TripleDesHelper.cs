using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    // source: https://www.cyberforum.ru/csharp-beginners/thread1370172.html#post7200863
    static class TripleDesHelper
    {
        private static byte[] IV = { 84, 69, 82, 78, 79, 86, 89, 73 };
        private static byte[] Key = { 118, 121, 115, 111, 107, 111, 112, 114, 101, 118, 111, 115, 107, 104, 111, 100, 105, 116, 101, 108, 115, 116, 118, 111 };

        /// <summary>
        /// Генерирует новые "ключи" - IV и Key.
        /// </summary>
        public static void GenerateIvAndKey()
        {
            using (TripleDES tdes = TripleDES.Create())
            {
                // Для работы TripleDES требуется вектор инициализации (IV) и ключ (Key)
                // Операции шифрования/деширования должны использовать одинаковые значения IV и Key
                tdes.GenerateIV();
                tdes.GenerateKey();
                IV = tdes.IV;
                Key = tdes.Key;
            }

        }

        /// <summary>
        /// Создает копию файла по указанному пути, шифрует её и сохраняет по заданному пути.
        /// </summary>
        /// <param name="inputFile">Путь к исходному файлу</param>
        /// <param name="outputFile">Путь для сохранения шифрованного файла</param>
        public static void EncryptFile(string inputFile, string outputFile)
        {
            bool replace = false;
            if (inputFile == outputFile)
            {
                replace = true;
                outputFile += ".tmp";
            }
            using (TripleDES tdes = TripleDES.Create())
            {

                // Для работы TripleDES требуется вектор инициализации (IV) и ключ (Key)
                // Операции шифрования/деширования должны использовать одинаковые значения IV и Key
                tdes.IV = TripleDesHelper.IV;
                tdes.Key = TripleDesHelper.Key;
                using (var inputStream = File.OpenRead(inputFile))
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                using (var encStream = new CryptoStream(outputStream, tdes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    outputStream.SetLength(0);
                    inputStream.CopyTo(encStream);
                }
            }
            if (replace)
            {
                File.Delete(inputFile);
                File.Move(outputFile, inputFile);
            }
        }

        /// <summary>
        /// Создает копию шифрованного файла по указанному пути, ДЕшифрует её и сохраняет по заданному пути.
        /// </summary>
        /// <param name="inputFile">Путь к исходному файлу</param>
        /// <param name="outputFile">Путь для сохранения шифрованного файла</param>
        public static void DecryptFile(string inputFile, string outputFile)
        {
            using (TripleDES tdes = TripleDES.Create())
            {
                tdes.IV = TripleDesHelper.IV;
                tdes.Key = TripleDesHelper.Key;
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
            using (TripleDES tdes = TripleDES.Create())
            {
                tdes.IV = TripleDesHelper.IV;
                tdes.Key = TripleDesHelper.Key;

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
            using (TripleDES tdes = TripleDES.Create())
            {
                tdes.IV = TripleDesHelper.IV;
                tdes.Key = TripleDesHelper.Key;

                byte[] encryptedByteBuff = Convert.FromBase64String(encryptedString);

                // Испльзуем ДЕкриптор.
                byte[] byteBuff = tdes.CreateDecryptor().TransformFinalBlock(
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
            using (TripleDES tdes = TripleDES.Create())
            {
                tdes.IV = TripleDesHelper.IV;
                tdes.Key = TripleDesHelper.Key;
                using (var inputStream = File.OpenRead(inputFile))
                using (var decStream = new CryptoStream(inputStream, tdes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    // convert stream to string
                    StreamReader reader = new StreamReader(decStream);
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
