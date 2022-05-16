using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.Models
{
    public class AnsAndWork
    {
        /// <summary>
        /// Номер задачи.
        /// </summary>
        public string NumTask { get; set; }
        /// <summary>
        /// Задание.
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// Текст павильного варианта.
        /// </summary>
        public string AnsRight { get; set; }
        /// <summary>
        /// Номер правильного варианта.
        /// </summary>
        public string AnsRightid { get; set; }
        /// <summary>
        /// Текст ответа ученика.
        /// </summary>
        public string AnsStud { get; set; }
        /// <summary>
        /// Номер ответа ученика.
        /// </summary>
        public string AnsStudid { get; set; }

    }
}
