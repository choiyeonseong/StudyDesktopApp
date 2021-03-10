using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyHistoryApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            TreeNode root = new TreeNode("영국 왕실");

            TreeNode stuart = new TreeNode("스튜어트 가");
            stuart.Nodes.Add("ANN1", "앤 (1707~1714)");  // 눈에 보이는값과 안보이는 값 나누기 (Name, Text)

            TreeNode hanover = new TreeNode("하노버 가");
            hanover.Nodes.Add("GRG1", "조지 1세(1714~1727)");  
            hanover.Nodes.Add("GRG2", "조지 2세(1727~1760)");
            hanover.Nodes.Add("GRG3", "조지 3세(1760~1820)");
            hanover.Nodes.Add("GRG4", "조지 4세(1820~1830)");
            hanover.Nodes.Add("WRM4", "윌리엄 4세(1830~1837)");
            hanover.Nodes.Add("BTR1", "빅토리아(1837~1901)");

            root.Nodes.Add(stuart);
            root.Nodes.Add(hanover);

            TrvList.Nodes.Add(root);
            TrvList.ExpandAll();    // 모든 트리 확장
        }

        private void TrvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(e.Node.ToString());
            PcbPhoto.Image = null;  // picturebox 초기화
            TxtDescription.Text = string.Empty; // descriptiontext 초기화

            if (e.Node.Name == "ANN1")
            {
                PcbPhoto.Image = Bitmap.FromFile("./Images/Anne.jpg");
                TxtDescription.Text = "앤 여왕은 1702년 3월 8일 잉글랜드와 스코틀랜드, 아일랜드의 여왕으로 즉위하였다." +
                    "Lorem ipsum dolor sit amet consectetur adipisicing elit." +
                    " Dolorum porro ullam magni amet incidunt eos numquam accusamus consequuntur reiciendis sapiente, " +
                    "corrupti iusto obcaecati unde ipsum, expedita illum dolor, quam velit.";
            }
            else if (e.Node.Name == "GRG1")
            {
                PcbPhoto.Image = Bitmap.FromFile("./images/King_George_I.jpg");
                TxtDescription.Text = "조지 1세는 1702년 3월 8일 잉글랜드와 스코틀랜드, 아일랜드의 여왕으로 즉위하였다." +
                    "Lorem ipsum dolor sit amet consectetur adipisicing elit." +
                    " Dolorum porro ullam magni amet incidunt eos numquam accusamus consequuntur reiciendis sapiente, " +
                    "corrupti iusto obcaecati unde ipsum, expedita illum dolor, quam velit.";
            }
        }
    }
}
