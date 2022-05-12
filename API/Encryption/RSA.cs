using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    class RSA
    {
        //private Encryption.RSA rsa;
        // Наш приватный ключ (клиентский) - для расшифровке данных от него.
        //static string mpr = "<RSAKeyValue><Modulus>s+yU1Dnrs2s+rrVTsSMDdKNhlN6sGtxAcxlovCrruHu0b040sAWU8IGnRuBTK4+PalzBmqAF8UXsu0ZpWNMsxaRwUbSEGKptqB1GWZy3E1RewAYdE3AmH+PMD9IWCiMa38r50mxMkChY3IcKXZDrKU38ANVsp5/fYZ9Tld4lQewk4GyrCd9JTjSbvgBlpa+oT2nTgc12hmdL+yaCH3KNKH67DTiRQZQBNiUcBFHMNVRB7mcV5aRmjFQzsxb5lFRXLcE9a0JvlQADkCNW12pJ+1tD9r1Rkm7HZL6fZ8vTknV06fXsYEwIRDFiLxuwx4NkOmduadV4cTDlh7cZ2mw6KQ==</Modulus><Exponent>AQAB</Exponent><P>6cHdAMpiYn8oA4wNEVPd0w9a13yRA1S9U9icF5hK1USx9xKbGXJQMwEBFkJ6PmBvc5dwHvrg1E0KzVVfPJEKcBw0ALAEOBO4jilxc/tNVJE33Z+y65qcASnlJtCk7RE8EkfsHYzePoTIF6uwMLdpRqCUSgEq+ZXYSGg1uEYac2M=</P><Q>xQtjCO07zbJhaJ2uPEO9DrbN0Pyc+hIghdEkucb7J63+XBLNKQSNrw6//dg8Qd8f0Oq0kyNk9HqZIaEMEks4AwateARu9BYjRwmyu3DpnLko2vQgz5cYXUe0dPlo0e2Xzb4IK+Eslc65dTVTMOpraWCDbE542t3HMZ0RStm1oAM=</Q><DP>PVPZUZ3HViQaMWQLAaLJLdh3ctWBjigrS9YmjaIs7+sH7dR50KFHHTqEOHzUaY0PcHootlgHqGtWDCz7wX4kCOm9UvjbAoNaBiIlW9JwpWE8EO8XF+0GRN/i+N4AhxngcsNf1RjiLfDUNjGbFgzol9kFTq0jwztm9QgyH9h0lBU=</DP><DQ>WP7rm/yKJj2LAVU2fMGbginOi7WMsOW/CrGLhNz6UtGjMLLNNtl3pLlkvaIMlTPRiup25H33+vPkdevnJ9wRUEyTzMEz28qj5FHdCAvXd5GpgjsBFsGHn5njR3HehfJpveM2jZsGKCNWYDnVeSJnoRCWSzSmITRJ436tcMEj50E=</DQ><InverseQ>s7K8sVaEiVCK/49KT76qGl0o0gqdeuztFqnr6yDawsDzXFfLmIFaaRS4aQCDhMLYEvR78q7SYvkbnupaIE0n1verHlh01aB3SST8nt88dsUm7jKJp5+ZtdvsnuixP8e29UiGX3Nj9988oLOUi6A9cTdahzit9wDOdSK5Ez2jL5Q=</InverseQ><D>MUH1xNxOkQItWPs/fGFPkuEf1/fWOkxOQLSWHd3KRulnDonklsQQcH+uYhML8gMNilr2jfR9a/5uvx6ZvL2jpkBMl7HJ8E7IUpwhJGtpG5ufjQafczDi3xwoQ7SKYCAYPWh+WINZuqWxHj7CMbd5CgfVlAzaAFIef8V0xrTYkmE0Di/Lwo8b5uNe1ULNQR2i9PSoaWXvbRjd+TbT2ujYWkK145r91zVg9cuw9t2TQCe8EaVjV7Opka2IQ/bjeB09x1RXsIyxsmVnZAJeslSOVXViL6QvjS+7h8V8ZfUiAQDdmEZya8w6AgzBYu34zSI1KPOQPWuVr9feDQnOzyvs2Q==</D></RSAKeyValue>";
        // Публичный ключ сервера - для шифрования данных при отправке ему.
        //static string spu = "<RSAKeyValue><Modulus>23jyUwQLUDQQPSiY0J6A+/2hybQFinBhxfN6fWUtmSIRgwI/YX+HDVIlz5PPsqzhygrgFtE1jZOHnTelJqImozo/QcHnuMosg4cXoS9ASG1A1ClAqldK55A15OKMPgb+J/AnatZEiID63xm9hftGGOlbzHmaZzpMmHEZS+hHnRx18JfhFR7S8D8qHpFoF9kw2EZEijjHbzn2JE7T5PDnX865h7ylVBEkBOPWVnuXo1hqvYPuhIlpiTTYq+Oti/GUeM0VWEPgNpfWHc3Birhd7EqjdMs0ajjkiV4mXkLyuZtjupl/2P1bwz/Qb6Al5jl+8Kb6xCtZ2D0fxzn4cCjaoQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public int KeySizeInBit { get; set; } = 2048;

        /// <summary>
        /// Возвращает или задаёт информацию только о публичном ключе.
        /// </summary>
        public RSAParameters MyPublicKey { get; set; }

        /// <summary>
        /// Возвращает или задаёт только публичный ключ в формате XML.
        /// </summary>
        public string MyXMLPublicKey
        {
            get 
            {
                string res;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
                {
                    RSA.ImportParameters(MyPublicKey);
                    res = RSA.ToXmlString(false);
                }
                return res;
            }
            set 
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
                    {
                        RSA.FromXmlString(value);
                        MyPublicKey = RSA.ExportParameters(false);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт информацию о публичном и приватном ключах.
        /// </summary>
        public RSAParameters PrivateKey { get; set; }
        
        /// <summary>
        /// Возвращает или задаёт публичный и приватный ключи в формате XML.
        /// </summary>
        public string MyXMLPrivateKey
        {
            get
            {
                string res;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
                {
                    RSA.ImportParameters(PrivateKey);
                    res = RSA.ToXmlString(true);
                }
                return res;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
                    {
                        RSA.FromXmlString(value);
                        PrivateKey = RSA.ExportParameters(true);
                    }
                }
            }
        }

        /// <summary>
        /// Открытый ключ, переданный сервером. Им шифруются сообщения серверу.
        /// </summary>
        public RSAParameters YourPublicKey { get; set; }
        /// <summary>
        /// Возвращает или задаёт открытый ключ, переданный сервером в формате XML.
        /// </summary>
        public string YourXMLPublicKey
        {
            get
            {
                string res;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
                {
                    RSA.ImportParameters(YourPublicKey);
                    res = RSA.ToXmlString(false);
                }
                return res;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
                    {
                        RSA.FromXmlString(value);
                        YourPublicKey = RSA.ExportParameters(false);
                    }
                }
            }
        }


        /// <summary>
        /// Генерирует новые ключи.
        /// </summary>
        public void GenerateKeys()
        {
            // Безопасно создаём объект типа RSACryptoServiceProvider для работы с RSA.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
            {
                // Получаем параметры приватного и публичного ключей.
                PrivateKey = RSA.ExportParameters(true);
                // Получаем параметры только публичного ключа.
                MyPublicKey = RSA.ExportParameters(false);
            }
        }

        /// <summary>
        /// Определяет - была ли проиницилазирована структура параметров ключа RSA.
        /// </summary>
        /// <param name="param">Параметры ключа RSA</param>
        /// <returns>True - если не инициализирована</returns>
        private bool parameterIsNull(RSAParameters param)
        {
            return param.Modulus == null &&
                param.Exponent == null &&
                param.P == null &&
                param.Q == null &&
                param.DP == null &&
                param.DQ == null &&
                param.InverseQ == null &&
                param.D == null;
        }

        /// <summary>
        /// Шифрование строки алгоритмом RSA.
        /// </summary>
        /// <param name="toEncode">Строка для шифрования.</param>
        /// <returns>Возвращает зашифрованную строку.</returns>
        public string EncodeToRSA(string toEncode, bool fOAEP = false)
        {
            if (string.IsNullOrWhiteSpace(toEncode)) return toEncode;
            if (parameterIsNull(YourPublicKey))
                throw new Exception("Публичный ключ не был указан!");

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
            {
                try
                {
                    // Используем, полученный от сервера, публичный ключ.
                    RSA.ImportParameters(YourPublicKey);
                    // Кодирует строку в массив байт.
                    byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(toEncode);
                    // МАКСИМАЛЬНЫЙ ПАЗМЕР: 245 байт.
                    if (toEncodeAsBytes.Length > 245)
                        throw new Exception("Исходная строка для шифрования слишком длинная!");
                    // Шифруем массив байт.
                    byte[] encryptedData = RSA.Encrypt(toEncodeAsBytes, fOAEP);
                    // Получаем зашифрованную строку, попутно конвертируя в Base64.
                    string encryptedString = Convert.ToBase64String(encryptedData);
                    // Возвращаем.
                    return encryptedString;
                }

                // Отлавливаем и отображаем ошибки типа "CryptographicException".
                catch (Exception e)
                {
                    // TODO: вместо консоли надо их обрабатывать!!!
                    Console.WriteLine(e.Message);

                    return null;
                }
            }
        }

        public string DecodeFromRSA(string toDecode, bool fOAEP = false)
        {
            if (string.IsNullOrWhiteSpace(toDecode)) return toDecode;
            if (parameterIsNull(PrivateKey))
                throw new Exception("Ключи не были созданы или заданы!");

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(KeySizeInBit))
            {
                try
                {
                    // Используем, наш приватный ключ.
                    RSA.ImportParameters(PrivateKey);
                    // Конвертируем из Base64 в, зашифрованный RSA-алгоритмом, массив байт.
                    byte[] encryptedData = Convert.FromBase64String(toDecode);
                    // Дешифруем аллгоритмом RSA, получяя расшифрованный массив байт.
                    byte[] toDecodeAsBytes = RSA.Decrypt(encryptedData, fOAEP);
                    // Кодирует массив байт в строку.
                    string decryptedString = Encoding.UTF8.GetString(toDecodeAsBytes);
                    // Возвращаем результат.
                    return decryptedString;
                }
                // Отлавливаем и отображаем ошибки типа "CryptographicException".
                catch (CryptographicException e)
                {
                    // TODO: вместо консоли надо их обрабатывать!!!
                    Console.WriteLine(e.Message);

                    return null;
                }
            }
        }

        /// <summary>
        /// Сохраняет ключи в файлы. Можно указать необходимость шифрования файлов,
        /// тогда они будут зашифрованы алгоритмом TripleDES, с заданными в программе 
        /// ключами. Можно указать каталог. Если кактлог не задан, то файлы сохраняются
        /// рядом с файлом программы.
        /// </summary>
        /// <param name="encrypt">Надо ли их шифровать</param>
        /// <param name="pathToSave">Путь для сохранения</param>
        public void SaveKeys(bool encrypt, string pathToSave = null)
        {
            string path;
            // Если путь не задан, то оставляем его пустым.
            if (pathToSave == null) { path = ""; }
            // Если путь задан, то запоминаем его.
            else path = pathToSave;

            // Получаем полный путь к приватному ключу.
            string pathToPrivKey = Path.Combine(path, "/privkey");
            // Получаем полный путь к публичному ключу.
            string pathToPubKey = Path.Combine(path, "/pubkey");

            // Получаем текущий приватный ключ.
            string sPrivateKey = MyXMLPrivateKey;
            // Получаем текущий публичный ключ.
            string sPublicKey = MyXMLPublicKey;

            // Если надо шифровать.
            if (encrypt)
            {
                // Шифруем приватный ключ.
                sPrivateKey = TripleDesHelper.EncryptString(sPrivateKey);
                // Шифруем публичный ключ.
                sPublicKey = TripleDesHelper.EncryptString(sPublicKey);
            }
            // Записываем в файлы ключи.
            File.WriteAllText("privkey", sPrivateKey);
            File.WriteAllText("pubkey", sPublicKey);

        }

        /// <summary>
        /// Загружает ключи из файлов.
        /// </summary>
        /// <param name="pathToPrivKey"></param>
        /// <param name="pathToPubKey"></param>
        /// <param name="ItsEncrypted"></param>
        public void LoadKeys(string pathToPrivKey, string pathToPubKey, bool ItsEncrypted)
        {
            if (!File.Exists(pathToPrivKey) || !File.Exists(pathToPubKey))
            {
                throw new Exception("При загрузке ключей произошла ошибка: ключ(и) не найден(ы)!");
            }

            // Считываем ключи из файлов.
            string sPrivateKey = File.ReadAllText(pathToPrivKey);
            string sPublicKey = File.ReadAllText(pathToPubKey);

            // Если надо дешифровать.
            if (ItsEncrypted)
            {
                // Дешифруем приватный ключ.
                sPrivateKey = TripleDesHelper.DecryptString(sPrivateKey);
                // Дешифруем публичный ключ.
                sPublicKey = TripleDesHelper.DecryptString(sPublicKey);
            }

            // Задаем текущий приватный ключ.
            MyXMLPrivateKey = sPrivateKey;
            // Задаем текущий публичный ключ.
            MyXMLPublicKey = sPublicKey;
        }
    }
}
