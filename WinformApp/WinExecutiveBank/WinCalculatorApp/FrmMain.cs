using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCalculatorApp
{

    public partial class FrmMain : Form
    {
        public double Saved { get; set; }   // 연산자 이전 숫자 저장
        public double Memory { get; set; }
        public bool MemFlag { get; set; }
        public bool PercentFlag { get; set; }
        public char Op { get; set; }
        public bool OpFlag { get; set; }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            TxtExp.Text = TxtResult.Text = "";
            BtnMC.Enabled = BtnMR.Enabled = false;
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var str = btn.Text; // 0~9

            TxtResult.Text += str;
        }

        private void BtnOp_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            Saved = double.Parse(TxtResult.Text);   // 숫자만 가능 
            TxtExp.Text += $"{TxtResult.Text} {btn.Text} ";
            Op = btn.Text[0];   // "+\0" 에서 "+"만 
            OpFlag = true;
            PercentFlag = true;

            TxtResult.Text = "";
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            var value = double.Parse(TxtResult.Text);
            switch (Op)
            {
                case '＋':
                    TxtResult.Text = (Saved + value).ToString();
                    break;
                case '－':
                    TxtResult.Text = (Saved - value).ToString();
                    break;
                case '×':
                    TxtResult.Text = (Saved * value).ToString();
                    break;
                case '÷':
                    TxtResult.Text = (Saved / value).ToString();
                    break;
            }
        }

        // 초기화
        private void BtnC_Click(object sender, EventArgs e)
        {
            TxtResult.Text = TxtExp.Text = "";
            Saved = 0;
            Op = '\0';
            OpFlag = false;
            PercentFlag = false;
        }

        // Memory Clear
        private void BtnMC_Click(object sender, EventArgs e)
        {
            TxtResult.Text = "";
            Memory = 0;
            BtnMR.Enabled = BtnMC.Enabled = false;
        }

        // Memory Read
        private void BtnMR_Click(object sender, EventArgs e)
        {
            TxtResult.Text = Memory.ToString();
            MemFlag = true;
        }

        // Memory Plus
        private void BtnMplus_Click(object sender, EventArgs e)
        {
            Memory += Double.Parse(TxtResult.Text);
        }

        // Memory Minus
        private void BtnMminus_Click(object sender, EventArgs e)
        {
            Memory -= Double.Parse(TxtResult.Text);

        }

        // Memory Save
        private void BtnMS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtResult.Text)) return;

            Memory = double.Parse(TxtResult.Text);
            BtnMC.Enabled = BtnMR.Enabled = true;
            MemFlag = true; 
        }

        private void BtnDot_Click(object sender, EventArgs e)
        {
            if (TxtResult.Text.Contains("."))
                return;
            else TxtResult.Text += ".";
        }

        // txtResult값 = 0
        private void BtnCE_Click(object sender, EventArgs e)
        {
            TxtResult.Text = "0";
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TxtResult.Text = TxtResult.Text.Remove(TxtResult.Text.Length - 1);
            if (TxtResult.Text.Length == 0)
                TxtResult.Text = "0";
        }

        private void BtnSqrt_Click(object sender, EventArgs e)
        {
            TxtExp.Text = "√(" + TxtResult.Text + ") ";
            TxtResult.Text = Math.Sqrt(double.Parse(TxtResult.Text)).ToString();
        }

        private void BtnSqr_Click(object sender, EventArgs e)
        {
            TxtExp.Text = "sqr(" + TxtResult.Text + ") ";
            TxtResult.Text = (double.Parse(TxtResult.Text) * double.Parse(TxtResult.Text)).ToString();
        }

        private void BtnRecip_Click(object sender, EventArgs e)
        {
            TxtExp.Text = "1 / (" + TxtResult.Text + ") ";
            TxtResult.Text = (1 / double.Parse(TxtResult.Text)).ToString();
        }

        private void BtnSign_Click(object sender, EventArgs e)
        {
            double v = double.Parse(TxtResult.Text);
            TxtResult.Text = (-v).ToString();
        }

        private void BtnPercent_Click(object sender, EventArgs e)
        {
            if (PercentFlag == true)
            {
                double p = double.Parse(TxtResult.Text);
                p = Saved * p / 100.0;
                TxtResult.Text = p.ToString();
                TxtExp.Text += TxtResult.Text;
                PercentFlag = false;
            }
        }
    }
}
