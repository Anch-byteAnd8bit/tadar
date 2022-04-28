using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tadar
{
    /// <summary>
    /// Логика взаимодействия для marks.xaml
    /// </summary>
    public partial class marks : Page
    {
        UserSecViewModel uservm;
        public marks()
        {
            InitializeComponent();
            sss.DataContext = uservm = new UserSecViewModel();
           
            tas.DataContext = uservm = new UserSecViewModel();
            //Journ.DataContext = custDataTable;
            //journclass.ItemsSource = LoadCollectionData();

        }
        

        //void AddTab(object sender, RoutedEventArgs e)
        //{
        //    markofclass.Items.Add(new TabItem
        //    {
        //        Header = new TextBlock { Text = "Ноутбуки" }, // установка заголовка вкладки
        //       ///* Content = notesList /*// установка содержимого вкладки
        //       /////добавление в базу
        //    });
        //}
        //void RemTab(object sender, RoutedEventArgs e)
        //{
            
        //       markofclass.Items.Remove(markofclass.SelectedItem);
        //    //удаление из базы
        //}

        //private void btnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    TabItem tab_item = new TabItem();
        //    markofclass.Items.Add(tab_item);

        //    Label label = new Label();
        //    label.Content = "New Tab";
        //    tab_item.Header = label;

        //    Label content = new Label();
        //    content.Content = "This is the new tab's content";
        //    tab_item.Content = content;
        //}
        //        


        //            // Removes the selected tab:  
        //tabControl1.TabPages.Remove(tabControl1.SelectedTab);  
        //// Removes all the tabs:  
        //tabControl1.TabPages.Clear();



        public class Author
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime DOB { get; set; }
            public string BookTitle { get; set; }
            public bool IsMVP { get; set; }
        }
        private List<Author> LoadCollectionData()
        {
            List<Author> authors = new List<Author>();
            authors.Add(new Author()
            {
                ID = 101,
                Name = "Mahesh Chand",
                BookTitle = "Graphics Programming with GDI+",
                DOB = new DateTime(1975, 2, 23),
                IsMVP = false
            });

            authors.Add(new Author()
            {
                ID = 201,
                Name = "Mike Gold",
                BookTitle = "Programming C#",
                DOB = new DateTime(1982, 4, 12),
                IsMVP = true
            });

            authors.Add(new Author()
            {
                ID = 244,
                Name = "Mathew Cochran",
                BookTitle = "LINQ in Vista",
                DOB = new DateTime(1985, 9, 11),
                IsMVP = true
            });

            return authors;
            
        }
       

    }
}
