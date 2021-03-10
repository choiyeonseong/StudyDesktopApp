using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradeCalcApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        ComboBox[] Grades;
        ComboBox[] Scores;
        TextBox[] Subjects;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            TxtSub1.Text = "사이버보안및포렌식";
            TxtSub2.Text = "인터넷 DB응용";
            TxtSub3.Text = "운영체제";
            TxtSub4.Text = "아동발달과 교육";
            TxtSub5.Text = "멀티미디어 응용";
            TxtSub6.Text = "C언어 및 실습";
            TxtSub7.Text = "IT융합캡스톤디자인";

            Grades = new ComboBox[] { CboGrade1, CboGrade2, CboGrade3, CboGrade4, CboGrade5, CboGrade6, CboGrade7 };
            Scores = new ComboBox[] { CboScore1, CboScore2, CboScore3, CboScore4, CboScore5, CboScore6, CboScore7 };
            Subjects = new TextBox[] { TxtSub1, TxtSub2, TxtSub3, TxtSub4, TxtSub5, TxtSub6, TxtSub7 };
            object[] score = new object[] { 1, 2, 3, 4, 5 };
            object[] grade = new object[] { "A+", "A0", "B+", "B0", "C+", "C0", "D+", "D0", "F" };

            foreach (var item in Grades)
            {
                item.Items.AddRange(grade);
                item.SelectedIndex = 0;
            }
            foreach (var item in Scores)
            {
                item.Items.AddRange(score);
                item.SelectedIndex = 0;
            }
        }

        private void BtnAveScore_Click(object sender, EventArgs e)
        {
            double totalScore = 0;
            double totalGrade = 0;

            for (int i = 0; i < Scores.Length; i++)
            {
                if (Subjects[i].Text!="")
                {
                    int score = int.Parse(Scores[i].SelectedItem.ToString());
                    totalScore += score;
                    totalGrade += score * GetGrade(Grades[i].SelectedItem.ToString());
                }
            }
            TxtAveScore.Text = (totalGrade / totalScore).ToString("0.00");
        }

        private double GetGrade(string v)
        {
            double grade = 0;

            switch (v)
            {
                case "A+": grade = 4.5; break;
                case "A0": grade = 4.0; break;
                case "B+": grade = 3.5; break;
                case "B0": grade = 3.0; break;
                case "C+": grade = 2.5; break;
                case "C0": grade = 2.0; break;
                case "D+": grade = 1.5; break;
                case "D0": grade = 1.0; break;
                default: grade = 0; break;
            }
            return grade;
        }
    }
}
