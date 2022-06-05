using System;
using System.Collections.Generic;
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

    }
}
