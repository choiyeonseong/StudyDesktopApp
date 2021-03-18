using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmBooksPopup : MetroForm
    {
        #region 전역변수 영역
        public int SelIdx { get; set; }
        public string SelName { get; set; }

        #endregion

        #region 이벤트 영역

        public FrmBooksPopup()
        {
            InitializeComponent();
        }
        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            RefreshData();  // 테이블 조회
        }
        private void FrmDivCode_Resize(object sender, EventArgs e)
        {
            
        }
        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (DgvData.SelectedRows.Count == 0)
            {
                MetroMessageBox.Show(this, "데이터를 선택하세요", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelIdx = (int)DgvData.SelectedRows[0].Cells[0].Value;
            SelName = DgvData.SelectedRows[0].Cells[4].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        #endregion

        #region 커스텀 메서드 영역

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

                    var query = @"SELECT b.Idx
                                        ,b.Author
	                                    ,d.Division
                                        ,d.Names as DivName
                                        ,b.Names
                                        ,b.ReleaseDate
                                    FROM dbo.bookstbl as b 
                                   INNER JOIN dbo.divtbl as d 
                                      ON b.Division = d.Division "; // 210318 Description 컬럼 추가

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet(); // DataSet : 가상의 DB (의 객체화)
                    adapter.Fill(ds, "bookstbl");

                    DgvData.DataSource = ds;
                    DgvData.DataMember = "bookstbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // DataGridView 컬럼(d.Division) 화면에서 안보이게
            var column = DgvData.Columns[2];    // d.Division(장르번호) 컬럼
            column.Visible = false;

            // 컬럼 스타일 변경
            column = DgvData.Columns[4];    // b.Names(도서명) 컬럼
            column.Width = 250;
            column.HeaderText = "도서명";

            column = DgvData.Columns[1];    // b.Author(저자) 컬럼
            column.Width = 200;

            column = DgvData.Columns[0];    // b.Idx(인덱스) 컬럼
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 50;

            DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion
    }
}
