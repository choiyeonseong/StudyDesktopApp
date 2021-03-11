using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextViewerApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            DlgSelectText.FileName = "Select a text file";
            DlgSelectText.Filter = "Textfiles (*.txt)|*.txt";   // 파일 필터링
            DlgSelectText.Title = "Open text file";
        }

        private void BtnSelectTxtFile_Click(object sender, EventArgs e)
        {
            if (DlgSelectText.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = DlgSelectText.FileName;

                    using (FileStream fs = File.Open(filePath, FileMode.Open))  // 따로 close 하지 않아도 됨
                    {
                        Process.Start("notepad.exe", filePath);
                        //Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", filePath);  // 경로에 띄어쓰기 인식 못함
                    }
                }
                catch (SecurityException ex)
                {
                    // 보안상 권한 제한이 있는 곳 알림
                    MessageBox.Show($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
        }
    }
}
