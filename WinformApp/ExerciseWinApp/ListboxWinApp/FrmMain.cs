using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListboxWinApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 살기좋은 도시 초기화(가장 기본적인 listbox item 추가 방법)
            LsbGoodCity.Items.Add("오스트리아, 빈");
            LsbGoodCity.Items.Add("호주, 멜버른");
            LsbGoodCity.Items.Add("일본, 오사카");
            LsbGoodCity.Items.Add("캐나다, 캘거리");
            LsbGoodCity.Items.Add("호주, 시드니");
            LsbGoodCity.Items.Add("캐나다, 벤쿠버");
            LsbGoodCity.Items.Add("일본, 도쿄");
            LsbGoodCity.Items.Add("캐나다, 토론토");
            LsbGoodCity.Items.Add("덴마크, 코펜하겐");
            LsbGoodCity.Items.Add("호주, 애들레이드");

            // 데이터 바인딩 방식 중 하나
            List<string> lstCountry = new List<string>
            {
                "미국","러시아","중국","영국","프랑스","일본","이스라엘","사우디아라비아",
                "UAE","스위스","한국","캐나다"
            };
            LsbHappyCounty.DataSource = lstCountry;

            // 리스트로 넣는 값에 대한 리스트 박스 초기화
            LsbHappyCounty.SelectedIndex = -1;  
            TxtIndexHappy.Text = string.Empty;
        }

        // 3개의 함수 합칠수 있음
        private void LsbGdpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 선택된 값 확인
            //MessageBox.Show(sender.ToString());
            var selItem = sender as ListBox;    // unboxing
            //MessageBox.Show($"{selItem.SelectedIndex} / {selItem.SelectedItem}");
            TxtIndexGdp.Text = selItem.SelectedIndex.ToString();
            TxtItemGdp.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }

        private void LsbGoodCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selItem = sender as ListBox;    // unboxing
            TxtIndexGood.Text = selItem.SelectedIndex.ToString();
            TxtItemGood.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }

        private void LsbHappyCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selItem = sender as ListBox;    // unboxing
            TxtIndexHappy.Text = selItem.SelectedIndex.ToString();
            TxtItemHappy.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }

        private void BtnInit_Click(object sender, EventArgs e)
        {
            // index 초기화
            LsbGdpCountry.SelectedIndex
                = LsbGoodCity.SelectedIndex
                = LsbHappyCounty.SelectedIndex
                = -1;
            // textbox index 초기화
            TxtIndexGdp.Text
                = TxtIndexGood.Text
                = TxtIndexHappy.Text
                = string.Empty;
        }
    }
}
