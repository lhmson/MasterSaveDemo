using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using MasterSaveDemo.Model;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace MasterSaveDemo.ViewModel
{
    class BaoCaoMoDong_PrintPreview_ViewModel: BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        private bool _IsButtonEnabled { get; set; }
        public bool IsButtonEnabled
        {
            get { return _IsButtonEnabled; }
            set
            {
                _IsButtonEnabled = value;
                OnPropertyChanged("IsButtonEnabled");
            }
        }
        public BaoCaoMoDong_PrintPreview_ViewModel()
        {
            IsButtonEnabled = true;
            CloseWindowCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) => {
                var ex = p as Window;
                ex.Close();
                
            });
        }
       
    }
}
