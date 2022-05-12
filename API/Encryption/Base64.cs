using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    static class Base64
    {

        /// <summary>
        /// Шифрует строку алгоритмом Base64, используя Unicode.
        /// </summary>
        /// <param name="toEncode">Строка для шифрования</param>
        /// <returns>Зашифрованная строка</returns>
        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }
}
