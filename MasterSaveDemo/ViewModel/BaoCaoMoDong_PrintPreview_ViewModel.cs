﻿using System;
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
    public class BaoCaoMoDong_PrintPreview_ViewModel : BaseViewModel
    {
        //---------------



        //---------------
        private string _ThangBaoCao;

        public string ThangBaoCao
        {
            get { return _ThangBaoCao; }
            set { _ThangBaoCao = value; OnPropertyChanged(); }
        }
        //---------------
        private string _NamBaoCao;

        public string NamBaoCao
        {
            get { return _NamBaoCao; }
            set { _NamBaoCao = value; OnPropertyChanged(); }
        }
        //---------------
        private string _NgayLapBaoCao;

        public string NgayLapBaoCao
        {
            get { return _NgayLapBaoCao; }
            set { _NgayLapBaoCao = value; OnPropertyChanged(); }
        }
        //---------------
        private string _MaBaoCao;

        public string MaBaoCao
        {
            get { return _MaBaoCao; }
            set { _MaBaoCao = value; OnPropertyChanged(); }
        }
        //---------------
        private string _LoaiTietKiem;

        public string LoaiTietKiem
        {
            get { return _LoaiTietKiem; }
            set { _LoaiTietKiem = value; OnPropertyChanged(); }
        }

        //---------------
        private List<ListBaoCaoDongMo> _ListBaoCaoDMPP;

        public List<ListBaoCaoDongMo> ListBaoCaoDMPP
        {
            get { return _ListBaoCaoDMPP; }
            set { _ListBaoCaoDMPP = value; OnPropertyChanged(); }
        }
        //--------------

        public ICommand CloseWindowCommand { get; set; }

        public BaoCaoMoDong_PrintPreview_ViewModel(string MaBC, string Thang, string Nam, string LTK, List<ListBaoCaoDongMo> list)
        {
            ListBaoCaoDMPP = list;
            NamBaoCao = Nam;
            MaBaoCao = MaBC;
            ThangBaoCao = Thang;
            LoaiTietKiem = LTK;
            NgayLapBaoCao = DateTime.Now.ToString("dd/MM/yyyy");
            CloseWindowCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) => {
                var ex = p as Window;
                ex.Close();

            });


        }

    }

}