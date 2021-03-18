using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmMain : MetroForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {   
            // 로그인 창 생성
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();

            LblMyName.Text = $"{frm.MyName} 님 접속";
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) // 사용자 편의를 위해
        {
            if (MetroMessageBox.Show(this, "종료하시겠습니까?", "종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;    // 프로그램 종료 안함
            }
        }

        /// <summary>
        /// MDI Child Form 생성
        /// </summary>
        /// <param name="form"></param>
        /// <param name="strTitle"></param>
        private void InitChildForm(Form form, string strTitle)
        {
            form.Text = strTitle;
            form.Dock = DockStyle.Fill;
            form.MdiParent = this;   // this = FrmMain
            form.FormBorderStyle = FormBorderStyle.None;

            // TODO : 사이즈 조절
            form.Width = 1875;//this.ClientSize.Width - 10;
            form.Height = 930;//this.Height - menuStrip1.Height;

            form.Show();
            form.WindowState = FormWindowState.Normal;
        }

        private void MnuDivCode_Click(object sender, EventArgs e)
        {
            FrmDivCode frm = new FrmDivCode();
            InitChildForm(frm, "구분코드 관리");
        }

        private void MnuMember_Click(object sender, EventArgs e)
        {
            FrmMember frm = new FrmMember();
            InitChildForm(frm, "회원 관리");
        }

        private void MnuBooks_Click(object sender, EventArgs e)
        {
            FrmBooks frm = new FrmBooks();
            InitChildForm(frm, "도서 관리");
        }

        private void MnuRental_Click(object sender, EventArgs e)
        {
            FrmRental frm = new FrmRental();
            InitChildForm(frm, "대여 관리");
        }

    }
}
