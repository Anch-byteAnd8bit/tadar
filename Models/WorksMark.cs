using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.Models
{
    internal class WorksAnswers
    {
        public WorksAnswers(Works works, Answers answers)
        {
            this.works = works;
            this.answers = answers;
        }

        public Works works;
        public Answers answers;

        public Works Works
        {
            get { return works; }
            set { works = value; }
        }
        
        public Answers Answers
        {
            get { return answers; }
            set { answers = value; }
        }

    }
}
