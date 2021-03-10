using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreCalcApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            int.TryParse(TxtKorean.Text, out int korean);
            int.TryParse(TxtMath.Text, out int math);
            int.TryParse(TxtEnglish.Text, out int english);
            int total = korean + math + english;
            double average = total / 3;
            TxtTotal.Text = total.ToString();
            TxtAverage.Text = average.ToString("0.0");
        }
    }
}
