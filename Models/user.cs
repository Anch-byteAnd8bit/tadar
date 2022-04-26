using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class User
    
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public virtual List<Mark> Marks { get; set; }

        public User(string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
        }
        public User() { }




    }
   
    class Mark
    {
        public DateTime Dateoftest { get; set; }
        public string Nameoftest { get; set; }
        public string Markoftest { get; set; }

        public Mark(DateTime dateoftest, string nameoftest, string markoftest)
        {
            this.Dateoftest = dateoftest;
            this.Nameoftest = nameoftest;
            this.Markoftest = markoftest;
        }
        public Mark() { }
    }



}
