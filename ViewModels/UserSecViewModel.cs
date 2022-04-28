using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tadar.Models;
using Tadar.ViewModels;

namespace Tadar.ViewModels
{
    class UserSecViewModel: BaseViewModel
    {

        public ObservableCollection<UserViewModel> UsersList { get; set; }
        public ObservableCollection<PerfomingWorkViewModel> WorksList { get; set; }
        public ObservableCollection<MarkViewModel> MarksList1 { get; set; }
        public ObservableCollection<MarkViewModel> MarksList2 { get; set; }
        public ObservableCollection<MarkViewModel> MarksList3 { get; set; }
        public UserForRegistration userregis { get; set; }
        public UserSecViewModel(UserForRegistration userregis)
        { }
       


        //public UserSecViewModel(List<User> users)

        //{
        //    //UsersList = new ObservableCollection<userViewModel>(users.Select(b => new userViewModel(b)));
        //}
        public UserSecViewModel(List<Work> works)
        {}
        public UserSecViewModel(List<Mark> marks)
        { }
        Mark Ko = new Mark
        {
            NameOfTest = "sssss",
            MarkOfTest = "5",
            DateOfTest = DateTime.Parse("25.05.2022")
        };
        Mark Ko1 = new Mark
        {
            NameOfTest = "Sasha",
            MarkOfTest = null,
            DateOfTest = DateTime.Parse("25.05.2022")
        };
        public UserSecViewModel()
        {
            //UsersList = new ObservableCollection<userViewModel>(users.Select(b => new userViewModel(b)));
           

            //UsersList = new ObservableCollection<UserViewModel>
            //{
            //    new UserViewModel(
            //    new User {
            //        Age=22,
            //        Name="Anna",
            //        Surname="Chu",
            //         Marks = new List<Mark> { Ko, Ko1 }
            //    }),
            //    new UserViewModel(
            //    new User {
            //        Age=31,
            //        Name="Sasha",
            //        Surname="Rog",
            //        Marks = new List<Mark> { Ko, Ko1 }
            //    }),
            //    new UserViewModel(
            //    new User {
            //        Age=110,
            //        Name="Annanai",
            //        Surname="Chuvash",
            //        Marks = new List<Mark> { Ko, Ko1 }
            //    })
            //};




            WorksList = new ObservableCollection<PerfomingWorkViewModel>
            {
                new PerfomingWorkViewModel(
                new Work {
                    Date= DateTime.Parse("25.05.2022"),
                    Name="Подставьте падежные аффиксы",
                    Description="Плюс перевод"
                }),
                new PerfomingWorkViewModel(
                new Work {
                    Date= DateTime.Parse("25.05.2022"),
                    Name="Подставьте падежные аффиксы",
                    Description="Плюс перевод"
                }),
                new PerfomingWorkViewModel(
                new Work {
                    Date= DateTime.Parse("25.05.2022"),
                    Name="Подставьте падежные аффиксы",
                    Description="Плюс перевод"
                })
            };

           


            //MarksList1 = new ObservableCollection<markvm>
            //{
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest="5"
            //    }),
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest=null
            //    }),
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest=null
            //    })
            //};
            //MarksList2 = new ObservableCollection<markvm>
            //{
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте падежные аффиксы",
            //        Markoftest="4"
            //    }),
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest=null
            //    }),
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest=null
            //    })
            //};
            //MarksList3 = new ObservableCollection<markvm>
            //{
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте падежные аффиксы",
            //        Markoftest="4"
            //    }),
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest=null
            //    }),
            //    new markvm(
            //    new mark {
            //        Dateoftest= DateTime.Parse("25.05.2022"),
            //        Nameoftest="Подставьте",
            //        Markoftest=null
            //    })
            //};

        }



        
        
    }
}
