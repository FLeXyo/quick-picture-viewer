﻿namespace quick_picture_viewer
{
	partial class WallpaperForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WallpaperForm));
			this.okButton = new System.Windows.Forms.Button();
			this.titlePanel = new System.Windows.Forms.Panel();
			this.closeBtn = new QuickLibrary.QlibTitlebarButton();
			this.titleLabel = new System.Windows.Forms.Label();
			this.fitLabel = new System.Windows.Forms.Label();
			this.fitComboBox = new QuickLibrary.QlibComboBox();
			this.infoTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.titlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.FlatAppearance.BorderSize = 0;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.okButton.Image = ((System.Drawing.Image)(resources.GetObject("okButton.Image")));
			this.okButton.Location = new System.Drawing.Point(10, 113);
			this.okButton.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(300, 32);
			this.okButton.TabIndex = 3;
			this.okButton.Text = " set background";
			this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.okButton.UseVisualStyleBackColor = false;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// titlePanel
			// 
			this.titlePanel.Controls.Add(this.closeBtn);
			this.titlePanel.Controls.Add(this.titleLabel);
			this.titlePanel.Location = new System.Drawing.Point(0, 0);
			this.titlePanel.Margin = new System.Windows.Forms.Padding(0);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new System.Drawing.Size(320, 32);
			this.titlePanel.TabIndex = 0;
			// 
			// closeBtn
			// 
			this.closeBtn.DarkImage = global::quick_picture_viewer.Properties.Resources.black_close;
			this.closeBtn.DarkMode = false;
			this.closeBtn.FlatAppearance.BorderSize = 0;
			this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
			this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.closeBtn.ForeColor = System.Drawing.Color.Black;
			this.closeBtn.Image = global::quick_picture_viewer.Properties.Resources.black_close;
			this.closeBtn.IsRed = true;
			this.closeBtn.LightImage = global::quick_picture_viewer.Properties.Resources.white_close;
			this.closeBtn.Location = new System.Drawing.Point(288, 0);
			this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(32, 32);
			this.closeBtn.TabIndex = 1;
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Location = new System.Drawing.Point(10, 7);
			this.titleLabel.Margin = new System.Windows.Forms.Padding(0, 9, 0, 9);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(101, 19);
			this.titleLabel.TabIndex = 16;
			this.titleLabel.Text = "WallpaperForm";
			// 
			// fitLabel
			// 
			this.fitLabel.AutoSize = true;
			this.fitLabel.Location = new System.Drawing.Point(10, 42);
			this.fitLabel.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
			this.fitLabel.Name = "fitLabel";
			this.fitLabel.Size = new System.Drawing.Size(82, 19);
			this.fitLabel.TabIndex = 15;
			this.fitLabel.Text = "choose a fit:";
			// 
			// fitComboBox
			// 
			this.fitComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.fitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fitComboBox.FormattingEnabled = true;
			this.fitComboBox.ItemHeight = 26;
			this.fitComboBox.Location = new System.Drawing.Point(10, 71);
			this.fitComboBox.Margin = new System.Windows.Forms.Padding(0);
			this.fitComboBox.Name = "fitComboBox";
			this.fitComboBox.Size = new System.Drawing.Size(300, 32);
			this.fitComboBox.TabIndex = 2;
			// 
			// WallpaperForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.ClientSize = new System.Drawing.Size(320, 155);
			this.Controls.Add(this.fitLabel);
			this.Controls.Add(this.fitComboBox);
			this.Controls.Add(this.titlePanel);
			this.Controls.Add(this.okButton);
			this.Draggable = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WallpaperForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "WallpaperForm";
			this.TitleLabel = this.titleLabel;
			this.Load += new System.EventHandler(this.WallpaperForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WallpaperForm_KeyDown);
			this.titlePanel.ResumeLayout(false);
			this.titlePanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Panel titlePanel;
		private QuickLibrary.QlibComboBox fitComboBox;
		private System.Windows.Forms.Label fitLabel;
		private System.Windows.Forms.Label titleLabel;
		private System.Windows.Forms.ToolTip infoTooltip;
		private QuickLibrary.QlibTitlebarButton closeBtn;
	}
}
