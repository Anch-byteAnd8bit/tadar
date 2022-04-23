using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class usersecvm: ViewModelBase
    {


        public ObservableCollection<userViewModel> UsersList { get; set; }
        public usersecvm(List<user> users)
        {
            //UsersList = new ObservableCollection<userViewModel>(users.Select(b => new userViewModel(b)));
        }
        public usersecvm()
        {
            //UsersList = new ObservableCollection<userViewModel>(users.Select(b => new userViewModel(b)));


            UsersList = new ObservableCollection<userViewModel>
            {
                new userViewModel(
                new user {
                    Age=22,
                    Name="Anna",
                    Surname="Chu"
                }),
                new userViewModel(
                new user {
                    Age=31,
                    Name="Sasha",
                    Surname="Rog"
                }),
                new userViewModel(
                new user {
                    Age=110,
                    Name="Annanai",
                    Surname="Chuvash"
                })
            };


        }

        
        //public BookViewModel(Book book)
        //{
        //    this.Book = book;
        //}

        //public string Title
        //{
        //    get { return Book.Title; }
        //    set
        //    {
        //        Book.Title = value;
        //        OnPropertyChanged("Title");
        //    }
        //}

        //public string Author
        //{
        //    get { return Book.Author; }
        //    set
        //    {
        //        Book.Author = value;
        //        OnPropertyChanged("Author");
        //    }
        //}

        //public int Count
        //{
        //    get { return Book.Count; }
        //    set
        //    {
        //        Book.Count = value;
        //        OnPropertyChanged("Count");
        //    }
        //}
    }
}
