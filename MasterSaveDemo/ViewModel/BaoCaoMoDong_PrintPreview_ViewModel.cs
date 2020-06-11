using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using MasterSaveDemo.Model;
using System.Windows.Input;

namespace MasterSaveDemo.ViewModel
{
    class BaoCaoMoDong_PrintPreview_ViewModel: BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public BaoCaoMoDong_PrintPreview_ViewModel()
        {
            CloseWindowCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               ;
           });
        }
       
    }
}
