using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace WpfLoginApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string connString = "Data Source=127.0.0.1;Initial Catalog=PMS;Persist Security Info=True;" +
            "User ID=sa;Password=mssql_p@ssw0rd!";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            TxtUserId.Focus();  // 커서 포커스
        }

        private void TxtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // id에서 enter -> password에 focus
                TxtPassword.Focus();
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // password에서 enter -> login button 실행
                BtnLogin_Click(sender, e);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                try
                {
                    string query = $"SELECT count(*) " +
                                   $"  FROM Member" +
                                   $" WHERE UserId = '{TxtUserId.Text}' " +
                                   $"   AND Password='{TxtPassword.Password}'; ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = Convert.ToInt32(cmd.ExecuteScalar());  // parameter가 object

                    if (result == 1)
                    {
                        MessageBox.Show("로그인 성공");
                    }
                    else
                    {
                        MessageBox.Show("로그인 실패");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"예외 발생 : {ex.Message}");
                    return;
                }
            }
        }
    }
}
