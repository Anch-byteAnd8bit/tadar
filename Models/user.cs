using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class user
    
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public user(string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
        }
        public user() { }




    }
    //public  class ex

    //{
      
    //    public List<users> redd = new List<users>();
        
    //           //Name="Anna",
    //           //Surname="SSS",
    //           //Age=22
           
       
        
    //}
    

}
