using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmBooks : MetroForm
    {
        #region 전역변수 영역

        private bool IsNew = false;    // false : 수정 / true : 신규

        #endregion

        #region 이벤트 영역

        public FrmBooks()
        {
            InitializeComponent();
        }
        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true;   // 신규성 확인
            InitCboData();  // 콤보박스 데이터 초기화
            RefreshData();  // 테이블 조회

            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
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

                    var query = @"DELETE FROM [dbo].[bookstbl]
                                        WHERE Idx = @Idx";

                    cmd.Connection = conn;
                    cmd.CommandText = query;    // == SqlCommand cmd = new SqlCommand(conn, query);

                    // TODO : sqlParameter 생성
                    var pIdx = new SqlParameter("@Idx", SqlDbType.Int);
                    pIdx.Value = TxtIdx.Text;
                    cmd.Parameters.Add(pIdx);

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
            DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;    // 로드될때 선택된 데이터가 없도록
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

                    var query = @"SELECT b.Idx
                                        ,b.Author
	                                    ,d.Division
                                        ,d.Names as DivName
                                        ,b.Names
                                        ,b.ReleaseDate
                                        ,b.ISBN
                                        ,format(b.Price,'#,#') as Price
                                        ,b.Descriptions
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

            column = DgvData.Columns[8];    // b.Descriptions(설명) 컬럼
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

            column = DgvData.Columns[7];    // b.Idx(인덱스) 컬럼
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                    if (IsNew == true)  // INSERT
                    {
                        query = @"INSERT INTO dbo.bookstbl
                                        (Author
                                        ,Division
                                        ,Names
                                        ,ReleaseDate
                                        ,ISBN
                                        ,Price
                                        ,Descriptions)
                                    VALUES
                                        (@Author
			                            ,@Division
			                            ,@Names
			                            ,@ReleaseDate
			                            ,@ISBN
			                            ,@Price
			                            ,@Descriptions )";
                    }
                    else  // UPDATE
                    {
                        query = @"UPDATE dbo.bookstbl
                                     SET Author = @Author
                                        ,Division = @Division
                                        ,Names = @Names
                                        ,ReleaseDate = @ReleaseDate
                                        ,ISBN = @ISBN
                                        ,Price = @Price
                                        ,Descriptions = @Descriptions
                                   WHERE Idx = @idx ;";
                    }

                    cmd.Connection = conn;
                    cmd.CommandText = query;    // : SqlCommand cmd = new SqlCommand(conn, query);

                    // TODO : sqlParameter 생성
                    var pAuthor = new SqlParameter("@Author", SqlDbType.NVarChar, 50);
                    pAuthor.Value = TxtAuthor.Text;
                    cmd.Parameters.Add(pAuthor);

                    var pDivision = new SqlParameter("@Division", SqlDbType.VarChar, 8);
                    pDivision.Value = CboDivision.SelectedValue;    // B001
                    cmd.Parameters.Add(pDivision);

                    var pNames = new SqlParameter("@Names", SqlDbType.NVarChar, 100);
                    pNames.Value = TxtNames.Text;
                    cmd.Parameters.Add(pNames);

                    var pReleaseDate = new SqlParameter("@ReleaseDate", SqlDbType.DateTime);
                    pReleaseDate.Value = DtpReleaseDate.Value;
                    cmd.Parameters.Add(pReleaseDate);

                    var pISBN = new SqlParameter("@ISBN", SqlDbType.VarChar, 200);
                    pISBN.Value = TxtISBN.Text;
                    cmd.Parameters.Add(pISBN);

                    var pPrice = new SqlParameter("@Price", SqlDbType.Decimal);
                    pPrice.Value = TxtPrice.Text;
                    cmd.Parameters.Add(pPrice);

                    var pDescriptions = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                    pDescriptions.Value = TxtDescriptions.Text;
                    cmd.Parameters.Add(pDescriptions);

                    if (IsNew == false) // UPDATE 일때만 처리
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

        /// <summary>
        /// 입력 텍스트박스 초기화
        /// </summary>
        private void ClearInputs()
        {
            TxtIdx.Text = TxtAuthor.Text
                        = TxtNames.Text 
                        = TxtISBN.Text 
                        = TxtPrice.Text
                        = TxtDescriptions.Text = "";

            CboDivision.SelectedIndex = -1;
            DtpReleaseDate.Value = DateTime.Now;    // 오늘날짜로 초기화

            TxtIdx.ReadOnly = true;
            IsNew = true;
        }
        /// <summary>
        /// 입력값 유효성 체크 메서드
        /// </summary>
        /// <returns> 오류 생기면 return false </returns>
        private bool CheckValidation()
        {
            // Validation(유효성) 체크
            if (string.IsNullOrEmpty(TxtAuthor.Text) || 
                string.IsNullOrEmpty(TxtNames.Text) ||
                CboDivision.SelectedIndex == -1 ||
                DtpReleaseDate.Value == null)
            {
                MetroMessageBox.Show(this, "빈값은 처리할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 도서 장르 콤보박스 초기화
        /// </summary>
        private void InitCboData()
        {
            try
            {
                using (SqlConnection conn=new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = @"SELECT Division, Names 
                                    FROM dbo.divtbl";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader reader = cmd.ExecuteReader(); // query의 결과가 넘어옴
                    var temp = new Dictionary<string, string>();

                    while (reader.Read())   // 결과를 한줄씩 가져옴
                    {
                        temp.Add(reader[0].ToString(), reader[1].ToString()); // (Key)B001 , (Value)공포/스릴러
                    }

                    CboDivision.DataSource = new BindingSource(temp, null); // DataMember는 null로
                    CboDivision.DisplayMember = "Value";    // Value만 화면에 보이게 설정
                    CboDivision.ValueMember = "Key";    // 실제 값으로는 Key를 사용
                    CboDivision.SelectedIndex = -1;
                }
            }
            catch {}    //메시지 박스 필요없음
        }

        /// <summary>
        /// 선택된 데이터 정보를 텍스트박스에 출력
        /// </summary>
        /// <param name="selData"></param>
        private void AsignToControls(DataGridViewRow selData)
        {
            TxtIdx.Text = selData.Cells[0].Value.ToString();
            TxtAuthor.Text = selData.Cells[1].Value.ToString();
            CboDivision.SelectedValue = selData.Cells[2].Value; // B001 = B001
            TxtNames.Text = selData.Cells[4].Value.ToString();
            DtpReleaseDate.Value = (DateTime)selData.Cells[5].Value;
            TxtISBN.Text = selData.Cells[6].Value.ToString();
            TxtPrice.Text = selData.Cells[7].Value.ToString();
            TxtDescriptions.Text = selData.Cells[8].Value.ToString();
        }

        #endregion
    }
}
