using System;

namespace Tadar.Models
{
    class Work

    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        

        public Work(string name, DateTime date, string description)
        {
            this.Name = name;
            this.Date = date;
            this.Description = description;
        }
        public Work() { }




    }
   
   
}
