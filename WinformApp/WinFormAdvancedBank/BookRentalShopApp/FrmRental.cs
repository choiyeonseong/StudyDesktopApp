using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmRental : MetroForm
    {
        #region 전역변수 영역

        private bool IsNew = false;    // false : 수정 / true : 신규

        private int selMemberIdx = 0;   // 선택된 회원 번호
        private string selMemberName = "";  // 선택된 회원 이름
        private int selBookIdx = 0;
        private string selBookName = "";

        #endregion

        #region 이벤트 영역

        public FrmRental()
        {
            InitializeComponent();
        }
        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true;   // 신규성 확인
            InitCboData();  // 콤보박스 데이터 초기화
            RefreshData();  // 테이블 조회

            DtpRentalDate.CustomFormat = "yyyy-MM-dd";
            DtpRentalDate.Format = DateTimePickerFormat.Custom;
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
                AsignToControls(selData);

                IsNew = false;  // 수정
                BtnSearchBook.Enabled = BtnSearchMember.Enabled = false;
                DtpRentalDate.Enabled = false;
            }
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



        private void BtnSearchMember_Click(object sender, EventArgs e)
        {
            FrmMemberPopup frm = new FrmMemberPopup();
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selMemberIdx = frm.SelIdx;
                TxtMemberName.Text = selMemberName = frm.SelName;
            }
        }

        private void BtnSearchBook_Click(object sender, EventArgs e)
        {
            FrmBooksPopup frm = new FrmBooksPopup();
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selBookIdx = frm.SelIdx;
                TxtBookNames.Text = selBookName = frm.SelName;
            }
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

                    var query = @"SELECT r.Idx
                                        ,r.memberIdx
	                                    ,m.Names as memberName
                                        ,r.bookIdx
	                                    ,b.Names as bookName
                                        ,r.rentalDate
                                        ,r.returnDate
                                        ,r.rentalState
                                        ,case r.rentalState 
			                                    when 'R' then '대여중'
			                                    when 'T' then '반납'
			                                    else '상태 없음' 
		                                    end as StateDesc
                                    FROM dbo.rentaltbl as r
                              inner join membertbl as m
                                 on r.memberIdx = m.Idx
                              inner join bookstbl as b
                                 on r.bookIdx = b.Idx ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet(); // DataSet : 가상의 DB (의 객체화)
                    adapter.Fill(ds, "rentaltbl");

                    DgvData.DataSource = ds;
                    DgvData.DataMember = "rentaltbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // DataGridView 컬럼 화면에서 안보이게
            var column = DgvData.Columns[1];    // r.memberIdx 컬럼
            column.Visible = false;

            column = DgvData.Columns[3];    // r.bookIdx 컬럼
            column.Visible = false;

            column = DgvData.Columns[7];    // r.rentalState 컬럼
            column.Visible = false;

            // 컬럼 스타일 변경
            column = DgvData.Columns[0];    // r.Idx(인덱스) 컬럼
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 50;

            column = DgvData.Columns[4];    // b.Names(도서명) 컬럼
            column.Width = 200;

            column = DgvData.Columns[8];    // r.rentalState 컬럼
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.Width = 100;
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
                        query = @"INSERT INTO [dbo].[rentaltbl]
                                               ([memberIdx]
                                               ,[bookIdx]
                                               ,[rentalDate]
                                               ,[rentalState])
                                         VALUES
                                               (@memberIdx
                                               ,@bookIdx
                                               ,@rentalDate
                                               ,@rentalState ) ";
                    }
                    else  // UPDATE
                    {
                        query = @"UPDATE [dbo].[rentaltbl]
                                    SET [returnDate] = GETDATE()
                                        ,[rentalState] = 'T'
                                  WHERE Idx = @idx";
                    }

                    cmd.Connection = conn;
                    cmd.CommandText = query;    // : SqlCommand cmd = new SqlCommand(conn, query);

                    if (IsNew == true)  // INSERT 일때
                    {
                        var pMemberIdx = new SqlParameter("@memberIdx", SqlDbType.Int);
                        pMemberIdx.Value = selMemberIdx;
                        cmd.Parameters.Add(pMemberIdx);

                        var pBookIdx = new SqlParameter("@bookIdx", SqlDbType.Int);
                        pBookIdx.Value = selBookIdx;
                        cmd.Parameters.Add(pBookIdx);

                        var pRentalDate = new SqlParameter("@rentalDate", SqlDbType.Date);
                        pRentalDate.Value = DtpRentalDate.Value;
                        cmd.Parameters.Add(pRentalDate);

                        var pRentalState = new SqlParameter("@rentalState", SqlDbType.Char, 1);
                        pRentalState.Value = CboRentalState.SelectedValue;
                        cmd.Parameters.Add(pRentalState);
                    }
                    else   // UPDATE 일때
                    {
                        var pIdx = new SqlParameter("@Idx", SqlDbType.Int);
                        pIdx.Value = TxtIdx.Text;
                        cmd.Parameters.Add(pIdx);
                    }


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
        private void ClearInputs()
        {
            selMemberIdx = selBookIdx = 0;
            selMemberName = selBookName = "";

            TxtIdx.ReadOnly = true;

            TxtIdx.Text = TxtMemberName.Text = TxtBookNames.Text = "";
            DtpRentalDate.Value = DateTime.Now;    // 오늘날짜로 초기화
            TxtReturnDate.Text = "";
            CboRentalState.SelectedIndex = -1;

            BtnSearchBook.Enabled = BtnSearchMember.Enabled = true;
            DtpRentalDate.Enabled = true;

            IsNew = true;
        }
        /// <summary>
        /// 입력값 유효성 체크 메서드
        /// </summary>
        /// <returns> 오류 생기면 return false </returns>
        private bool CheckValidation()
        {
            // Validation(유효성) 체크
            if (string.IsNullOrEmpty(TxtMemberName.Text) ||
                string.IsNullOrEmpty(TxtBookNames.Text) ||
                DtpRentalDate.Value == null ||
                CboRentalState.SelectedIndex == -1 )
            {
                MetroMessageBox.Show(this, "빈값은 처리할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void InitCboData()
        {
            try
            {
                var temp = new Dictionary<string, string>();
                temp.Add("R", "대여중");
                temp.Add("T", "반납");

                CboRentalState.DataSource = new BindingSource(temp, null);
                CboRentalState.DisplayMember = "Value"; // Value만 화면에 보이게 설정
                CboRentalState.ValueMember = "Key";    // 실제 값으로는 Key를 사용
                CboRentalState.SelectedIndex = -1;
            }
            catch { }    //메시지 박스 필요없음
        }

        private void AsignToControls(DataGridViewRow selData)
        {
            TxtIdx.Text = selData.Cells[0].Value.ToString();
            selMemberIdx = (int)selData.Cells[1].Value;
            Debug.WriteLine($">>>> selMemberIdx : {selMemberIdx}");
            TxtMemberName.Text = selData.Cells[2].Value.ToString();
            selBookIdx = (int)selData.Cells[3].Value;
            Debug.WriteLine($">>>> selBookIdx : {selBookIdx}");
            TxtBookNames.Text = selData.Cells[4].Value.ToString();
            DtpRentalDate.Value = (DateTime)selData.Cells[5].Value;
            TxtReturnDate.Text = selData.Cells[6].Value == null ? "" : selData.Cells[6].Value.ToString();
            CboRentalState.SelectedValue = selData.Cells[7].Value;


        }


        #endregion


    }
}
