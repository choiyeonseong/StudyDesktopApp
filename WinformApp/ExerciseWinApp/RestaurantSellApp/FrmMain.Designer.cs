﻿
namespace RestaurantSellApp
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.CboRestaurant = new System.Windows.Forms.ComboBox();
            this.LblResult = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "좋아하는 식당 리스트";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(256, 39);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "추가";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // CboRestaurant
            // 
            this.CboRestaurant.FormattingEnabled = true;
            this.CboRestaurant.Location = new System.Drawing.Point(41, 42);
            this.CboRestaurant.Name = "CboRestaurant";
            this.CboRestaurant.Size = new System.Drawing.Size(165, 20);
            this.CboRestaurant.TabIndex = 2;
            this.CboRestaurant.SelectedIndexChanged += new System.EventHandler(this.CboRestaurant_SelectedIndexChanged);
            // 
            // LblResult
            // 
            this.LblResult.AutoSize = true;
            this.LblResult.Location = new System.Drawing.Point(39, 150);
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(109, 12);
            this.LblResult.TabIndex = 3;
            this.LblResult.Text = "이번 주 모임장소는";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(256, 68);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 4;
            this.BtnDelete.Text = "삭제";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 206);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.LblResult);
            this.Controls.Add(this.CboRestaurant);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "좋아하는 레스토랑";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ComboBox CboRestaurant;
        private System.Windows.Forms.Label LblResult;
        private System.Windows.Forms.Button BtnDelete;
    }
}
