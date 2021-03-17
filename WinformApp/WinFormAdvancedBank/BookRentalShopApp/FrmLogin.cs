using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmLogin : MetroForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var strUserId = ""; // select에서 받아와서 처리할 변수

            //MessageBox.Show("로그인 처리");
            if (string.IsNullOrEmpty(TxtUserId.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MetroMessageBox.Show(this, "아이디/패스워드를 입력하세요!", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // SqlConnection 연결
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "SELECT userID FROM membertbl " +
                                " WHERE userID = @userID " +
                                "   AND passwords = @passwords " +
                                "   AND levels = 's' ";       // @ : 파라미터 속성

                    // sqlCommand 생성
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // SQL Injection 해킹을 방지하기 위해 사용
                    SqlParameter pUserID = new SqlParameter("@userId", SqlDbType.VarChar, 20);
                    pUserID.Value = TxtUserId.Text;
                    cmd.Parameters.Add(pUserID);    // SqlParameter 개체를 SqlCommand 개체의 Parameters 속성(@)에 할당

                    SqlParameter pPasswords = new SqlParameter("@passwords", SqlDbType.VarChar, 20);
                    pPasswords.Value = TxtPassword.Text;
                    cmd.Parameters.Add(pPasswords); // SqlParameter 개체를 SqlCommand 개체의 Parameters 속성(@)에 할당

                    // SqlDataReader 실행(1)
                    SqlDataReader reader = cmd.ExecuteReader();

                    // reader로 처리...
                    reader.Read();
                    strUserId = reader["userID"] != null ? reader["userID"].ToString() : "";    // null이 아니면 값을 입력

                    // 중간점검
                    //MessageBox.Show(strUserId);

                    if(string.IsNullOrEmpty(strUserId))
                    {
                        MetroMessageBox.Show(this, "접속실패", "로그인실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "접속성공", "로그인성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"Error : {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, Height);
                return;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);    // 완전 종료(부모창 까지)
        }

        private void TxtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) TxtPassword.Focus();
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) BtnLogin_Click(sender, e);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}