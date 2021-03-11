﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoviePlayerApp
{
    public partial class FrmPlayer : Form
    {
        public FrmPlayer()
        {
            InitializeComponent();
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            if (DlgSelectFile.ShowDialog()==DialogResult.OK)
            {
                MoviePlayer.URL = DlgSelectFile.FileName;
            }
        }

        private void FrmPlayer_Load(object sender, EventArgs e)
        {
            MoviePlayer.stretchToFit = true;    // 영상 사이즈를 플레이어에 맞춤
        }
    }
}