using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsAPI.Helpers
{
    public static class Other
    {

        //https://stackoverflow.com/a/1404/1988244
        /// <summary>
        /// Высчитывает возраст.
        /// </summary>
        /// <param name="bday">Дата рождения.</param>
        /// <returns>Количество лет.</returns>
        public static int CalcAgeByBDate(DateTime bday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - bday.Year;
            if (bday > now.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Разбивает строку на заданное количество частей.
        /// </summary>
        /// <param name="s">Исходная строка.</param>
        /// <param name="maxLength">Максимальная длина одной части.</param>
        /// <returns>Список частей исходной строки</returns>
        public static List<string> SplitStr(string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s) || (maxLength > s.Length)) return new List<string> { s };
            if (maxLength <= 0) return null;

            List<string> res = new List<string>();
            int n = s.Length / maxLength;
            for (int i = 0; i < n; i++)
            {
                res.Add(s.Substring(i* maxLength, maxLength));
            }
            if (s.Length != maxLength) res.Add(s.Substring(n * maxLength));
            return res;
        }

        private static byte[] SubArrByte(byte[] arr, int index, int count = 0)
        {
            if (count <= 0) count = arr.Length - index;
            return arr.Skip(1 + index).Take(count).ToArray();
        }

        public static List<byte[]> SplitBuffByte(byte[] arr, int maxLength)
        {
            if ((arr == null) || (arr.Length == 0) || (maxLength > arr.Length)) return new List<byte[]> { arr };
            if (maxLength <= 0) return null;

            List<byte[]> res = new List<byte[]>();
            int n = arr.Length / maxLength;
            for (int i = 0; i < n; i++)
            {
                res.Add(SubArrByte(arr, i * maxLength, maxLength));
            }
            if (arr.Length != maxLength) res.Add(SubArrByte(arr, n * maxLength));
            return res;
        }
    }
}
