using MasterSaveDemo.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterSaveDemo.UnitTest
{
    [TestFixture]
    class ThayDoiQuyDinhTest
    {
        ThayDoiQuyDinh_ViewModel viewModel;

        [SetUp]
        public void Init()
        {
            viewModel = new ThayDoiQuyDinh_ViewModel();
        }

        [Test]
        public void TestHiddenPopupBox()
        {
            viewModel.HiddenPopupBox();

            Assert.IsTrue(viewModel.VisibilityPopup1 == Visibility.Hidden);
            Assert.IsTrue(viewModel.VisibilityPopup2 == Visibility.Hidden);
            Assert.IsTrue(viewModel.VisibilityPopup3 == Visibility.Hidden);
            Assert.IsTrue(viewModel.VisibilityPopup4 == Visibility.Hidden);
            Assert.IsTrue(viewModel.VisibilityPopup5 == Visibility.Hidden);
            Assert.IsTrue(viewModel.VisibilityPopup6 == Visibility.Hidden);
        }

        [TestCase("3 thang", "90", "5.5", "90", "Rut het", true, true)]
        [TestCase("  ", "90", "5.5", "90", "Rut het", true, false)]
        [TestCase("3 thang", "", "5.5", "90", "Rut het", true, false)]
        [TestCase("3 thang", "90", "", "90", "Rut het", true, false)]
        [TestCase("3 thang", "90", "5.5", "", "Rut het", true, false)]
        [TestCase("3 thang", "90", "5.5", "90", null, true, false)]
        public void TestCheckValidData_Add(string tenLoai, string kyHan, string laiSuat, string thoiGian, string quyDinh, bool flag, bool expectedRes)
        {
            viewModel.TenLoaiTietKiem = tenLoai;
            viewModel.KyHan = kyHan;
            viewModel.LaiSuat = laiSuat;
            viewModel.ThoiGianGuiToiThieu = thoiGian;
            viewModel.SelectedQuyDinh = quyDinh;

            viewModel.CheckValidData_Add(ref flag);

            Assert.AreEqual(expectedRes, flag);
        }
    }
}
