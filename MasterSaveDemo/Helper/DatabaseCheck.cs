using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using MasterSaveDemo.Model;


namespace MasterSaveDemo.Helper
{
    class DatabaseCheck
    {
        private SqlConnection conn = new SqlConnection(@"Server =.\sqlexpress; initial catalog = QLSTK; integrated security = True; MultipleActiveResultSets=True;App=EntityFramework");
        private static DatabaseCheck _ins;
        public static DatabaseCheck Ins
        {
            get
            {
                if (_ins == null) _ins = new DatabaseCheck();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        public DatabaseCheck()
        {

        }
        public void Check()
        {
            try
            {
                conn.Open();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 4060: // Invalid Database
                        DialogResult re = MessageBox.Show("Chưa có cơ sở dữ liệu, khởi tạo ngay bây giờ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (re == DialogResult.Yes)
                        {
                            TaoDatabase();
                        }
                        else
                        {
                            Application.Exit();
                        }
                        break;
                    case 18456: // Login Failed 

                        break;
                    default:

                        break;
                }
            }
        }
        public void TaoDatabase()
        {
            conn = new SqlConnection(@"Server=.\sqlexpress;Integrated security=SSPI;database=master");
            string str = "CREATE DATABASE QLSTK";
            SqlCommand myCommand = new SqlCommand(str, conn);
            try
            {
                conn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Đã thành công khởi tạo database!");
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = new SqlConnection(@"Server =.\sqlexpress; initial catalog = QLSTK; integrated security = True; MultipleActiveResultSets=True;App=EntityFramework");
            }
        }
        
    }
}
