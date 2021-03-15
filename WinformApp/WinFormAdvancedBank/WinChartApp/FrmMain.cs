using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinChartApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void BtnRegen_Click(object sender, System.EventArgs e)
        {
            GenNewChart();
        }

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            this.Text = "중간고사 성적";  // form의 제목
            ChtScore.Titles.Add("중간고사 성적"); // 차트의 제목

            GenNewChart();
        }

        private void GenNewChart()
        {
            Random rand = new Random();
            ChtScore.Series[0].Points.Clear();    // "Score" 시리즈 안의 데이터만 삭제
            for (int i = 0; i < 10; i++)
            {
                ChtScore.Series[0].Points.Add(rand.Next(10, 100));  // ["Score"] == [0]
            }
            ChtScore.Series[0].LegendText = "수학"; // 범례
            ChtScore.Series[0].ChartType = SeriesChartType.Column;  // 차트 모양 변경
        }
    }
}
