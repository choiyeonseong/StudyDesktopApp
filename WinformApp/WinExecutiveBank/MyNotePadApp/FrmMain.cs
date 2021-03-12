using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotePadApp
{
    public partial class FrmMain : Form
    {
        public bool IsModify { get; set; }  // 수정되었는지 확인하는 Flag

        private const string firstFileName = "noname.txt";

        private string curFileName = firstFileName;

        public FrmMain()
        {
            InitializeComponent();
        }
        
        private void FrmMain_Load(object sender, EventArgs e)
        {// 메인 로드
            this.Text = $"{curFileName} - 내 메모장";
            IsModify = false;
            DlgSaveText.Filter = DlgOpenText.Filter = "Text file (*.txt)|*.txt|Log file (*.log)|*.log";   // 확장자 지정
        }
       
        private void TxtMain_TextChanged(object sender, EventArgs e)
        { // 텍스트창 변경
            IsModify = true;
            this.Text = $"{curFileName}* - 내 메모장";
        }
      
        private void MnuNewFile_Click(object sender, EventArgs e)
        {  // 새로 만들기
            // TODO: 만약 작업중인 파일이 있으면 저장처리
            ProcessSaveFileBeforeClose();

            TxtMain.Text = "";
            IsModify = false;
            curFileName = firstFileName;
            this.Text = $"{curFileName} - 내 메모장";
        }
       
        private void ProcessSaveFileBeforeClose()
        { // 작업중인 파일 저장
            if (IsModify)
            {
                DialogResult answer = MessageBox.Show("변경사항이 있습니다. 저장하시겠습니까?", "저장",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    if (curFileName == firstFileName)
                    {
                        if (DlgSaveText.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter sw = File.CreateText(DlgSaveText.FileName);
                            sw.WriteLine(TxtMain.Text);
                            sw.Close();
                        }
                    }
                    else
                    {
                        StreamWriter sw = File.CreateText(curFileName);
                        sw.WriteLine(TxtMain.Text);
                        sw.Close();
                    }
                }
            }
        }
        
        private void MnuOpenFile_Click(object sender, EventArgs e)
        {// 열기
            ProcessSaveFileBeforeClose();   // 열기 전 저장

            DlgOpenText.ShowDialog();
            curFileName = DlgOpenText.FileName;
            this.Text = $"{curFileName} - 내 메모장";

            try
            {
                StreamReader sr = File.OpenText(curFileName);
                TxtMain.Text = sr.ReadToEnd();

                IsModify = false;
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void MnuSaveFile_Click(object sender, EventArgs e)
        { // 저장
            if (curFileName == firstFileName)
            {
                if (DlgSaveText.ShowDialog() == DialogResult.OK)
                    curFileName = DlgSaveText.FileName;
            }

            StreamWriter sw = File.CreateText(curFileName);
            sw.WriteLine(TxtMain.Text);

            IsModify = false;
            sw.Close(); // using사용하면 없어도 됨

            this.Text = $"{curFileName} - 내 메모장";
        }
      
        private void MnuExit_Click(object sender, EventArgs e)
        {  // 종료
            ProcessSaveFileBeforeClose();   // 종료 전 저장
            Environment.Exit(0);    // 프로그램 완전 종료
        }
     
        private void MnuCopy_Click(object sender, EventArgs e)
        {   // 복사하기
            var contents = ActiveControl as RichTextBox;
            if(contents!=null)
            {
                Clipboard.SetDataObject(contents.SelectedText);
                MessageBox.Show(contents.SelectedText);
            }
        }
       
        private void MnuPaste_Click(object sender, EventArgs e)
        { // 붙여넣기
            var contents = ActiveControl as RichTextBox;
            if (contents!=null)
            {
                IDataObject data = Clipboard.GetDataObject();
                contents.SelectedText = data.GetData(DataFormats.Text).ToString();
                IsModify = true;
            }
        }

        private void MnuAbout_Click(object sender, EventArgs e)
        {// 프로그램 정보
            //MessageBox.Show("메모장 v1.0입니다.");
            var form = new AboutThis(); // 어셈블리 info에서 바꾸면 됨
            form.ShowDialog();
        }
    }
}
