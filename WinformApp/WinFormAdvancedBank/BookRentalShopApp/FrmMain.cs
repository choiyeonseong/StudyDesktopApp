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

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
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

        private void MnuDivCode_Click(object sender, EventArgs e)
        {
            FrmDivCode frm = new FrmDivCode();
            frm.Dock = DockStyle.Fill;
            frm.MdiParent = this;   // this = FrmMain
            frm.Show();
            frm.Width = this.ClientSize.Width - 10;
            frm.Height = this.Height - menuStrip1.Height;
            frm.WindowState = FormWindowState.Maximized;
        }

        private void MnuMember_Click(object sender, EventArgs e)
        {
            FrmMember frm = new FrmMember();
            frm.Dock = DockStyle.Fill;
            frm.MdiParent = this;   // this = FrmMain
            frm.Show();
            frm.Width = this.ClientSize.Width - 10;
            frm.Height = this.Height - menuStrip1.Height;
            frm.WindowState = FormWindowState.Maximized;
        }
    }
}
