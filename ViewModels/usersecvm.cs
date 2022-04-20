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
        public ObservableCollection<usersvm> UsersList { get; set; }

        #region Constructor

        public usersecvm(List<users> users)
        {
            UsersList = new ObservableCollection<usersvm>(users.Select(b => new usersvm(b)));
        }

        #endregion
    }
}
