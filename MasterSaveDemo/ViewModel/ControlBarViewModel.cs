using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterSaveDemo.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        //public ICommand MouseMoveCommand { get; set; }
        #endregion

        public ControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                Window window = GetWindowParent(p);
                if (window != null)
                {
                    window.Close();
                }
            });
            
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                Window window = GetWindowParent(p);
                if (window != null)
                {
                    if (window.WindowState != WindowState.Minimized)
                        window.WindowState = WindowState.Minimized;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;
                }
            });

            //MouseMoveCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    Window window = GetWindowParent(p);
            //    if (window != null)
            //    {
            //        window.DragMove();
            //    }
            //});
        }

        public Window GetWindowParent(UserControl p)
        {
            return Window.GetWindow(p);
        }
    }
}
