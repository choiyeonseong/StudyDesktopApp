using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LableTestApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LblAutoSize.Text = LblManualSize.Text = string.Empty;   // empty lable text
        }

        private void BtnPushText_Click(object sender, EventArgs e)
        {
            string sample1 = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Quos iste ullam necessitatibus quidem quae quia non aperiam consequuntur ab, magni quis, totam eveniet officiis reprehenderit numquam a quo maxime veritatis!";
            string sample2 = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Beatae sequi excepturi labore tenetur totam, maxime dicta nesciunt velit ducimus, facere harum. Mollitia sit reiciendis eaque ut. Asperiores dolore ut aut!";

            LblAutoSize.Text = sample1;
            LblManualSize.Text = sample2;
        }
    }
}
