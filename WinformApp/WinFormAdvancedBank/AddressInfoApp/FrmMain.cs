using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AddressInfoApp
{
    public partial class FrmMain : Form
    {
        string connString = "Data Source=127.0.0.1;Initial Catalog=PMS;Persist Security Info=True;" +
            "User ID=sa;Password=mssql_p@ssw0rd!";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            RefreshData();
            ClearInput();
        }

        /// <summary>
        /// DataGrid 새로고침
        /// </summary>
        private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

                // SSMS 테이블 스크립팅 메뉴 활용
                string query = "SELECT Idx " +
                               "     , FullName " +
                               "     , Mobile " +
                               "     , Addr " +
                               "  FROM dbo.Address";

                // 1. SqlCommand, SqlDataReader or object 사용방법
                // 2. SqlDataAdapter, DataSet 사용방법
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DgvAddress.DataSource = ds.Tables[0];
            }
        }

        /// <summary>
        /// TextBox 초기화
        /// </summary>
        private void ClearInput()
        {
            TxtIdx.Text = TxtFullName.Text = TxtMobile.Text = TxtAddr.Text = "";
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            // Null값 방지
            if (string.IsNullOrEmpty(TxtFullName.Text) || string.IsNullOrEmpty(TxtMobile.Text))
            {
                MessageBox.Show("데이터를 입력하십시오");
                return;
            }

            // 중복데이터 추가 방지
            int.TryParse(TxtIdx.Text, out int result);
            if (result > 0)
            {
                MessageBox.Show("초기화를 하십시오.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = $"INSERT INTO dbo.Address " +
                               $" ( FullName, " +
                               $"   Mobile, " +
                               $"   Addr, " +
                               $"   RegId, " +
                               $"   RegDate ) " +
                               $" VALUES " +
                               $" ( '{TxtFullName.Text}', " +
                               $"   '{TxtMobile.Text.Replace("-", "")}', " +
                               $"   '{TxtAddr.Text}', " +
                               $"   'admin', " +
                               $"   GETDATE() ); ";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("입력 성공!");
                }
                else
                {
                    MessageBox.Show("입력 실패!");
                }
            }
            RefreshData();
            ClearInput();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // 선택된 데이터가 없을 때
            int.TryParse(TxtIdx.Text, out int result);
            if (result == 0)
            {
                MessageBox.Show("데이터를 선택하십시오");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = $"UPDATE Address " +
                               $" SET " +
                               $" FullName = '{TxtFullName.Text}', " +
                               $" Mobile = '{TxtMobile.Text.Replace("-", "")}', " +
                               $" Addr = '{TxtAddr.Text}', " +
                               $" ModId = 'admin', " +
                               $" ModDate= GETDATE() " +
                               $" WHERE Idx = {result} ";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("수정 성공!");
                }
                else
                {
                    MessageBox.Show("수정 실패!");
                }
            }
            RefreshData();
            ClearInput();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 선택된 데이터가 없을 때
            int.TryParse(TxtIdx.Text, out int result);
            if (result == 0)
            {
                MessageBox.Show("데이터를 선택하십시오");
                return;
            }

            // 삭제하시겠습니까? 메세지 박스
            if (MessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = $"DELETE FROM Address WHERE Idx = {result}";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("삭제 성공!");
                    }
                    else
                    {
                        MessageBox.Show("삭제 실패!");
                    }
                }
                RefreshData();
                ClearInput();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void TxtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)  // Enter
            {
                TxtAddr.Focus();
            }
        }

        private void TxtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)  // 13 == enter
            {
                TxtMobile.Focus();
            }
        }

        /// <summary>
        /// 선택된 셀의 데이터를 TextBox에 표시
        /// </summary>
        private void DgvAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var selData = DgvAddress.Rows[e.RowIndex].Cells;
                TxtIdx.Text = selData[0].Value.ToString();
                TxtFullName.Text = selData[1].Value.ToString();
                TxtMobile.Text = selData[2].Value.ToString();
                TxtAddr.Text = selData[3].Value.ToString();
            }
            catch (Exception ex)
            {
                // 인덱스를 벗어난 셀 선택
                MessageBox.Show(ex.Message);
            }
        }
    }
}
