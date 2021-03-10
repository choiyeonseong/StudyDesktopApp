﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListViewApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ListViewItem itemSwitch = new ListViewItem("Nintendo Switch", 0);
            itemSwitch.SubItems.Add("360,000");
            itemSwitch.SubItems.Add("10");
            itemSwitch.SubItems.Add("3600,000");

            ListViewItem itemDs = new ListViewItem("Nintendo DS", 1);
            itemDs.SubItems.Add("150,000");
            itemDs.SubItems.Add("20");
            itemDs.SubItems.Add("3000,000");

            ListViewItem itemPs = new ListViewItem("PlayStation 4", 2);
            itemPs.SubItems.Add("400,000");
            itemPs.SubItems.Add("10");
            itemPs.SubItems.Add("4000,000");

            ListViewItem itemWii = new ListViewItem("Nintendo Wii", 3);
            itemWii.SubItems.Add("200,000");
            itemWii.SubItems.Add("30");
            itemWii.SubItems.Add("6000,000");

            ListViewItem itemXBox = new ListViewItem("XBox 360", 4);
            itemXBox.SubItems.Add("700,000");
            itemXBox.SubItems.Add("20");
            itemXBox.SubItems.Add("14,000,000");

            LsvProducts.Items.AddRange(new ListViewItem[] { itemSwitch, itemDs, itemPs, itemWii, itemXBox });
        }

        private void RdbDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbDetails.Checked) LsvProducts.View = View.Details;
        }

        private void RdbList_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbList.Checked) LsvProducts.View = View.List;
        }

        private void RdbSmallIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbSmallIcon.Checked) LsvProducts.View = View.SmallIcon;
        }

        private void RdbLargeIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbLargeIcon.Checked) LsvProducts.View = View.LargeIcon;
        }

        private void LsvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtSelected.Text = string.Empty;
            var selected = LsvProducts.SelectedItems;

            foreach (ListViewItem item in selected)
            {
                for (int i = 0; i < 4; i++)
                {
                    TxtSelected.Text += item.SubItems[i].Text + " ";
                }
            }
        }
    }
}