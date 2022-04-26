using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.Helpers
{
    public static class ContentGenerator
    {
        public static DateTime RandomDayFunc()
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return start.AddDays(gen.Next(range));
        }

        public static UserForRegistration getRandUFR()
        {
            Random r = new Random();
            // Пользователь, которого мы регистрируем.
            return new UserForRegistration
            {
                BDate = RandomDayFunc().ToString("yyyy/MM/dd"),
                Email = Path.GetRandomFileName() + "@mail.ru",
                GenderID = r.Next(1, 3).ToString(),
                Login = Path.GetRandomFileName(),
                Surname = Path.GetRandomFileName(),
                Name = Path.GetRandomFileName(),
                Middlename = Path.GetRandomFileName(),
                Pass = Guid.NewGuid().ToString()
            };
        }
    }
}
