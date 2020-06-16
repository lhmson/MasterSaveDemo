using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Windows.Threading;

namespace MasterSaveDemo.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        //public ICommand MouseMoveCommand { get; set; }
        #endregion

        #region Variable
        private string _NgayGioHienTai;
        public string NgayGioHienTai
        {
            get => _NgayGioHienTai;
            set {
                if (_NgayGioHienTai == value)
                    return;
                _NgayGioHienTai = value; ; OnPropertyChanged(); }
        }

        public DispatcherTimer _timer;
        #endregion

        #region function
        private void GetTimeNow()
        {
            NgayGioHienTai = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            // Copy from https://stackoverflow.com/questions/31160201/time-ticking-in-c-sharp-wpf-mvvm
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                NgayGioHienTai = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            };
            _timer.Start();
        }
        #endregion

        public ControlBarViewModel()
        {
            
            GetTimeNow();
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
