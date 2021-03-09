using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorChangerApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Trackbar_Scroll(object sender, EventArgs e)
        {   // 하나의 작업을 하는 이벤트는 하나로 통일
            TxtRed.Text = TrbRed.Value.ToString();
            TxtGreen.Text = TrbGreen.Value.ToString();
            TxtBlue.Text = TrbBlue.Value.ToString();

            PnlResult.BackColor = Color.FromArgb(TrbRed.Value, TrbGreen.Value, TrbBlue.Value);
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            PnlResult.BackColor= colorDialog1.Color;
            TrbRed.Value = colorDialog1.Color.R;
            TrbGreen.Value = colorDialog1.Color.G;
            TrbBlue.Value = colorDialog1.Color.B;
            TxtRed.Text = TrbRed.Value.ToString();
            TxtGreen.Text = TrbGreen.Value.ToString();
            TxtBlue.Text = TrbBlue.Value.ToString();
        }
    }
}
