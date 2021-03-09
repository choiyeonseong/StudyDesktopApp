using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeWinApp.Views
{
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
        }

        private void FrmParent_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(800, 600);   // 부모창 사이즈
            this.CenterToScreen();  // 스크린의 가운데 위치

            FrmChild frm = new FrmChild();  // 자식창 생성
            this.AddOwnedForm(frm); // 항상 부모창 위에 자식창 생성

            frm.Show(); // 모달리스
            //frm.ShowDialog(); // 자식창을 꺼야 부모창이 표시
        }
    }
}
