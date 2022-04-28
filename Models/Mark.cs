using System;

namespace Tadar.Models
{
    class Mark
    {
        public DateTime DateOfTest { get; set; }
        public string NameOfTest { get; set; }
        public string MarkOfTest { get; set; }

        public Mark(DateTime dateOfTest, string nameOfTest, string markOfTest)
        {
            this.DateOfTest = dateOfTest;
            this.NameOfTest = nameOfTest;
            this.MarkOfTest = markOfTest;
        }
        public Mark() { }
    }
}
