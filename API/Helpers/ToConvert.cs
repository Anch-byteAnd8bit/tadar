using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Helpers
{
    public static class ToConvert
    {
        /// <summary>
        /// Конвертирование ДАТЫ И ВРЕМЕНИ типа DateTime в строку, подходящую для загрузки в БД.
        /// </summary>
        /// <param name="dt">Дата и время</param>
        /// <returns></returns>
        public static string DB_DateTimeToStringDT(DateTime dt) => dt.ToString("yyyy.MM.dd HH:mm:ss");

        /// <summary>
        /// Конвертирование ДАТЫ типа DateTime в строку, подходящую для загрузки в БД.
        /// </summary>
        /// <param name="d">Дата</param>
        /// <returns></returns>
        public static string DB_DateTimeToStringD(DateTime d) => d.ToString("yyyy.MM.dd");
        
        /// <summary>
        /// Конвертирование ВРЕМЕНИ типа DateTime в строку, подходящую для загрузки в БД.
        /// </summary>
        /// <param name="t">Дата</param>
        /// <returns></returns>
        public static string DB_DateTimeToStringT(DateTime t) => t.ToString("HH:mm:ss");
    }
}
