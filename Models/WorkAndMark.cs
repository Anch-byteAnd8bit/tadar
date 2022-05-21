using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.Models
{
    public class WorkAndMark
    {
        private Work work;
        private Answer answer;


        /// <summary>
        /// Конструктор. Обратите на подписи к параметрам.
        /// </summary>
        /// <param name="work">Либо TestWork, либо TextWork</param>
        /// <param name="answer">Либо TestAnswer, либо TextAnswer</param>
        public WorkAndMark(Work work, Answer answer)
        {
            this.work = work;
            this.answer = answer;
        }

        public string NameWork { get => work.WorkHeader.Name; }
        public string DescriptionWork { get => work.WorkHeader.Description; }
        public DateTime DateTimeEWork { get => DateTime.Parse(answer.AnswerHeader.DateTimeE); }
        public string Mark { get => answer.AnswerHeader.Mark; }
    }
}
