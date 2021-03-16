using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IoTSensorMonApp
{
    public partial class FrmMain : Form
    {

        private double xCount = 200;    // 차트에 보이는 데이터 수
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // ComboBox 초기화
            foreach (var port in SerialPort.GetPortNames())
            {
                CboSerialPort.Items.Add(port);  // ComboBox에 port이름 넣음
            }
            CboSerialPort.Text = "Select Port";

            // IoT장비에서 받을 값의 범위
            PrbPhotoResistor.Minimum = 0;
            PrbPhotoResistor.Maximum = 1023;
            PrbPhotoResistor.Value = 0;

            // 차트모양 초기화
            InitChartStyle();

            // BtnDisplay 초기화
            BtnDisplay.BackColor = Color.BlueViolet;
            BtnDisplay.ForeColor = Color.WhiteSmoke;
            BtnDisplay.Text = "NONE";
            BtnDisplay.Font = new Font("맑은 고딕", 14, FontStyle.Bold);

            // 나머지 초기화
            LblConnectTime.Text = "Connection Time :";
            TxtSensorNum.TextAlign = HorizontalAlignment.Right;
            TxtSensorNum.Text = "0";
            BtnConnect.Enabled = BtnDisconnect.Enabled = false;
        }

        /// <summary>
        /// 차트모양 초기화
        /// </summary>
        private void InitChartStyle()
        {
            // ChartArea 설정
            ChtPhotoResistors.ChartAreas[0].BackColor = Color.Navy;

            ChtPhotoResistors.ChartAreas[0].AxisX.Minimum = 0;
            ChtPhotoResistors.ChartAreas[0].AxisX.Maximum = xCount;
            ChtPhotoResistors.ChartAreas[0].AxisX.Interval = xCount / 4;
            ChtPhotoResistors.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            ChtPhotoResistors.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            ChtPhotoResistors.ChartAreas[0].AxisY.Minimum = 0;
            ChtPhotoResistors.ChartAreas[0].AxisY.Maximum = 1024;
            ChtPhotoResistors.ChartAreas[0].AxisY.Interval = 200;
            ChtPhotoResistors.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            ChtPhotoResistors.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            ChtPhotoResistors.ChartAreas[0].CursorX.AutoScroll = true;
            ChtPhotoResistors.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            ChtPhotoResistors.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            ChtPhotoResistors.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue;

            // Series 설정
            ChtPhotoResistors.Series.Clear();   // 기존의 Series1을 지움
            ChtPhotoResistors.Series.Add("PhotoValue");
            ChtPhotoResistors.Series[0].ChartType = SeriesChartType.Line;
            ChtPhotoResistors.Series[0].Color = Color.LightCoral;
            ChtPhotoResistors.Series[0].BorderWidth = 3;

            // 범례 삭제
            if (ChtPhotoResistors.Legends.Count > 0)
            {
                for (int i = 0; i < ChtPhotoResistors.Legends.Count; i++)
                {
                    ChtPhotoResistors.Legends.RemoveAt(i);
                }
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            // TODO : 나중에 실제 작업시 작성
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            // TODO : 나중에 실제 작업시 작성
        }

        private Timer timerSimul = new Timer();
        private Random randPhoto = new Random();
        private bool IsSimulation = false;    // flag
        private List<SensorData> sensors = new List<SensorData>();  // 차트, 리스트 박스에 출력

        /// <summary>
        /// 시뮬레이션 시작
        /// </summary>
        private void MnuBeginSimulation_Click(object sender, EventArgs e)
        {
            IsSimulation = true;
            timerSimul.Enabled = true;
            timerSimul.Interval = 100; // 0.1초에 한번 (1000 = 1초)
            timerSimul.Tick += TimerSimul_Tick;
            timerSimul.Start();
        }

        private void TimerSimul_Tick(object sender, EventArgs e)
        {
            int value = randPhoto.Next(1, 1023);    // 1~1023 사이의 값
            ShowSensorValue(value.ToString());
        }

        /// <summary>
        /// 센서값 화면 출력 메서드
        /// </summary>
        private void ShowSensorValue(string value)
        {
            int.TryParse(value, out int v);
            var currentTime = DateTime.Now;

            SensorData data = new SensorData(currentTime, v, IsSimulation);
            sensors.Add(data);

            // 센서값 갯수 표시
            TxtSensorNum.Text = sensors.Count.ToString();
            // 프로그레스바 현재값 출력
            PrbPhotoResistor.Value = v;
            // 리스트박스에 데이터 출력
            var item = $"{currentTime:yyyy-MM-dd HH:mm:ss}\t{v}";
            LsbPhotoResistors.Items.Add(item);
            LsbPhotoResistors.SelectedIndex = LsbPhotoResistors.Items.Count - 1;    // 스크롤 처리, 가장 최근 데이터 선택

            // 차트에 데이터 출력
            ChtPhotoResistors.Series[0].Points.Add(v);

            // 200개 이상 출력시 스크롤
        }

        /// <summary>
        /// 시뮬레이션 끝
        /// </summary>
        private void MnuEndSimulation_Click(object sender, EventArgs e)
        {
            IsSimulation = false;
            timerSimul.Stop();
        }

        /// <summary>
        /// 종료버튼 클릭
        /// </summary>
        private void MnuExit_Click(object sender, EventArgs e)
        {
            if (IsSimulation)
            {
                MessageBox.Show("시뮬레이션 멈춘 후 프로그램을 종료하세요", "종료",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            Environment.Exit(0);
        }
    }
}
