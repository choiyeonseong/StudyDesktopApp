using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmDivCode : MetroForm
    {
        #region 전역변수 영역

        private bool IsNew = false;    // false : 수정 / true : 신규

        #endregion

        #region 이벤트 영역

        public FrmDivCode()
        {
            InitializeComponent();
        }
        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true;
            RefreshData();
        }
        private void FrmDivCode_Resize(object sender, EventArgs e)
        {
            DgvData.Height = this.ClientRectangle.Height - 90;
            GrbDetail.Height = this.ClientRectangle.Height - 90;
        }
        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)    // 선택된 값이 존재하면
            {
                var selData = DgvData.Rows[e.RowIndex];
                TxtDivision.Text = selData.Cells[0].Value.ToString();
                TxtNames.Text = selData.Cells[1].Value.ToString();
                TxtDivision.ReadOnly = true;

                IsNew = false;  // 수정
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Validation(유효성) 체크 
            if (CheckValidation() == false) return;

            if (MetroMessageBox.Show(this, "삭제하시겠습니까?", "삭제",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            DeleteData();
            RefreshData();
            ClearInputs();
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation(유효성) 체크 
            if (CheckValidation() == false) return;

            SaveData();
            RefreshData();
            ClearInputs();
        }

        #endregion

        #region 커스텀 메서드 영역

        /// <summary>
        /// 삭제 처리 프로세스
        /// </summary>
        private void DeleteData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    SqlCommand cmd = new SqlCommand();

                    var query = "DELETE FROM [dbo].[divtbl] " +
                                     " WHERE [Division] = @Division";

                    cmd.Connection = conn;
                    cmd.CommandText = query;    // == SqlCommand cmd = new SqlCommand(conn, query);

                    // TODO : sqlParameter 생성
                    SqlParameter pDivison = new SqlParameter("@Division", SqlDbType.VarChar, 8);
                    pDivison.Value = TxtDivision.Text;
                    cmd.Parameters.Add(pDivison);

                    var result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        MetroMessageBox.Show(this, "삭제 성공", "삭제",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "삭제 실패", "삭제",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 데이터 그리드 뷰 새로고침
        /// </summary>
        private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "SELECT [Division] " +
                                "      ,[Names] " +
                                "  FROM [dbo].[divtbl] ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds, "divtbl");
                    DgvData.DataSource = ds;
                    DgvData.DataMember = "divtbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;    // 로드될때 선택된 데이터가 없도록
        }
        /// <summary>
        /// 입력 처리 프로세스
        /// </summary>
        private void SaveData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    SqlCommand cmd = new SqlCommand();

                    var query = "";

                    if (IsNew == true)
                    {
                        query = "INSERT INTO dbo.divtbl " +
                                " VALUES " +
                                " ( @Division, @Names ) ";
                    }
                    else
                    {
                        query = "UPDATE [dbo].[divtbl] " +
                                "   SET [Names] = @Names " +
                                " WHERE [Division] = @Division";
                    }

                    cmd.Connection = conn;
                    cmd.CommandText = query;    // : SqlCommand cmd = new SqlCommand(conn, query);

                    // TODO : sqlParameter 생성
                    SqlParameter pNames = new SqlParameter("@Names", SqlDbType.NVarChar, 45);
                    pNames.Value = TxtNames.Text;
                    cmd.Parameters.Add(pNames);

                    SqlParameter pDivison = new SqlParameter("@Division", SqlDbType.VarChar, 8);
                    pDivison.Value = TxtDivision.Text;
                    cmd.Parameters.Add(pDivison);

                    var result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        // 저장성공
                        MetroMessageBox.Show(this, "저장 성공", "저장",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 저장 실패
                        MetroMessageBox.Show(this, "저장 실패", "저장",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 입력 텍스트박스 초기화
        /// </summary>
        private void ClearInputs()
        {
            TxtDivision.Text = TxtNames.Text = "";
            TxtDivision.ReadOnly = false;
            IsNew = true;
        }
        /// <summary>
        /// 입력값 유효성 체크 메서드
        /// </summary>
        /// <returns> 오류 생기면 return false </returns>
        private bool CheckValidation()
        {
            // Validation(유효성) 체크
            if (string.IsNullOrEmpty(TxtDivision.Text) || string.IsNullOrEmpty(TxtNames.Text))
            {
                MetroMessageBox.Show(this, "빈값은 처리할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #endregion
    }
}
