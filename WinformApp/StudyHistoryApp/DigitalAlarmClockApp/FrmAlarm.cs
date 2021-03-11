using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace DigitalAlarmClockApp
{
    public partial class FrmAlarm : Form
    {
        private DateTime SetDay;
        private DateTime SetTime;
        private bool IsSetAlarm;    // Flag
        WindowsMediaPlayer mediaPlayer; // mp3 플레이어

        public FrmAlarm()
        {
            InitializeComponent();
        }

        private void FrmAlarm_Load(object sender, EventArgs e)
        {
            // 미디어 플레이어 초기화
            mediaPlayer = new WindowsMediaPlayer();

            // 초기화 작업
            LblAlarm.ForeColor = Color.Gray; // 글자 색상

            LblDate.Text = LblTime.Text = string.Empty; // 시작할때 글자를 지워줌

            // CustomFormat 설정 - 속성창에서 설정하는 것과 같음
            DtpAlarmTime.Format = DateTimePickerFormat.Custom;
            DtpAlarmTime.CustomFormat = "hh:mm:ss";
            DtpAlarmTime.ShowUpDown = true; // 날짜설정 드롭다운 아닌 위아래 화살표 생김

            // 타이머 설정
            MyTimer.Interval = 1000;    // 1sec
            MyTimer.Tick += MyTimer_Tick;
            MyTimer.Enabled = true;
            MyTimer.Start();

            // 탭 설정
            TabClock.SelectedTab = TapDigitalClock;
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            DateTime curDate = DateTime.Now;
            LblDate.Text = curDate.ToShortDateString(); // 날짜 표시
            LblTime.Text = curDate.ToString("hh:mm:ss");  // 시간 표시

            if (IsSetAlarm == true)    // 알람 설정이 되었다면 
            {
                // 알람 시간하고 현재시간 일치하면 알람울림
                if (SetDay == DateTime.Today &&
                    SetTime.Hour == curDate.Hour &&
                    SetTime.Minute == curDate.Minute &&
                    SetTime.Second == curDate.Second)
                {
                    //IsSetAlarm = false; // 알람 끄기
                    BtnRelease_Click(sender,e); // 이벤트 핸들러 메소드로 사용 가능

                    // 알람 소리 재생
                    mediaPlayer.URL = @".\media\alarm.mp3";
                    mediaPlayer.controls.play();

                    MessageBox.Show("알람!!!", "알람", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            SetDay = DateTime.Parse(DtpAlarmDate.Text);
            SetTime = DateTime.Parse(DtpAlarmTime.Text);

            LblAlarm.Text = $"Alarm : {SetDay.ToShortDateString()} {SetTime.ToString("hh:mm:ss")}";
            LblAlarm.ForeColor = Color.Red; // 알람 설정되면 회색 -> 빨간색

            TabClock.SelectedTab = TapDigitalClock; // 디지털 시계 탭으로 이동
            IsSetAlarm = true;
        }

        private void BtnRelease_Click(object sender, EventArgs e)
        {
            IsSetAlarm = false;
            LblAlarm.Text = "Alarm : ";
            LblAlarm.ForeColor = Color.Gray;    // 알람 꺼지면 다시 빨간색 -> 회색
            TabClock.SelectedTab = TapDigitalClock; // 디지털 시계 탭으로 이동
        }
    }
}
