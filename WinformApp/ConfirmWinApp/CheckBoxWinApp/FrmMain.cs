using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckBoxWinApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            string checkStates = string.Empty;
            List<CheckBox> boxes = new List<CheckBox>
            {
                ChkApple, ChkPear, ChkStrawberry, ChkBanana, ChkOrange, ChkPeach
            };

            foreach (var item in boxes)
            {
                string checkStr = "별로..";
                if (item.Checked)
                    checkStr = "좋아!!";

                checkStates += $"{item.Text} :\t{checkStr}\n";
            }

            MessageBox.Show(checkStates, "체크상태");

            string summary = $"좋아하는 과일은 : ";

            foreach (var item in boxes)
            {
                if (item.Checked)   // if (item.Checked == true)
                    summary += item.Text + " ";
            }

            MessageBox.Show(summary, "좋아하는 과일 리스트");
        }
    }
}
