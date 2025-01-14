﻿namespace quick_picture_viewer
{
	partial class InfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.extensionTextBox = new QuickLibrary.QlibTextBox();
			this.extensionLabel = new System.Windows.Forms.Label();
			this.copyPathButton = new System.Windows.Forms.Button();
			this.copyFolderButton = new System.Windows.Forms.Button();
			this.copyNameButton = new System.Windows.Forms.Button();
			this.compressionTextBox = new QuickLibrary.QlibTextBox();
			this.compressionLabel = new System.Windows.Forms.Label();
			this.fullPathTextBox = new QuickLibrary.QlibTextBox();
			this.fullPathLabel = new System.Windows.Forms.Label();
			this.folderTextBox = new QuickLibrary.QlibTextBox();
			this.fileNameTextBox = new QuickLibrary.QlibTextBox();
			this.folderLabel = new System.Windows.Forms.Label();
			this.sizeTextBox = new QuickLibrary.QlibTextBox();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.megapixelsTextBox = new QuickLibrary.QlibTextBox();
			this.megapixelsLabel = new System.Windows.Forms.Label();
			this.resolutionTextBox = new QuickLibrary.QlibTextBox();
			this.resolutionLabel = new System.Windows.Forms.Label();
			this.inchesTextBox = new QuickLibrary.QlibTextBox();
			this.inchesLabel = new System.Windows.Forms.Label();
			this.cmTextBox = new QuickLibrary.QlibTextBox();
			this.cmLabel = new System.Windows.Forms.Label();
			this.ratioLabel = new System.Windows.Forms.Label();
			this.ratioTextBox = new QuickLibrary.QlibTextBox();
			this.diskSizeTextBox = new QuickLibrary.QlibTextBox();
			this.diskSizeLabel = new System.Windows.Forms.Label();
			this.modifiedLabel = new System.Windows.Forms.Label();
			this.modifiedTextBox = new QuickLibrary.QlibTextBox();
			this.createdLabel = new System.Windows.Forms.Label();
			this.createdTextBox = new QuickLibrary.QlibTextBox();
			this.propertiesButton = new System.Windows.Forms.Button();
			this.infoTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.titlePanel = new System.Windows.Forms.Panel();
			this.titleLabel = new System.Windows.Forms.Label();
			this.closeBtn = new QuickLibrary.QlibTitlebarButton();
			this.titlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fileNameLabel.Location = new System.Drawing.Point(10, 48);
			this.fileNameLabel.Margin = new System.Windows.Forms.Padding(0);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(102, 19);
			this.fileNameLabel.TabIndex = 27;
			this.fileNameLabel.Text = "file:";
			this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// extensionTextBox
			// 
			this.extensionTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.extensionTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.extensionTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.extensionTextBox.DarkMode = false;
			this.extensionTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.extensionTextBox.ForeColor = System.Drawing.Color.Black;
			this.extensionTextBox.Location = new System.Drawing.Point(304, 168);
			this.extensionTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.extensionTextBox.Name = "extensionTextBox";
			this.extensionTextBox.ReadOnly = true;
			this.extensionTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.extensionTextBox.Size = new System.Drawing.Size(70, 32);
			this.extensionTextBox.TabIndex = 9;
			this.extensionTextBox.Text = "N/A";
			this.extensionTextBox.WordWrap = true;
			// 
			// extensionLabel
			// 
			this.extensionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.extensionLabel.Location = new System.Drawing.Point(202, 174);
			this.extensionLabel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.extensionLabel.Name = "extensionLabel";
			this.extensionLabel.Size = new System.Drawing.Size(92, 19);
			this.extensionLabel.TabIndex = 51;
			this.extensionLabel.Text = "extension:";
			this.extensionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// copyPathButton
			// 
			this.copyPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.copyPathButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.copyPathButton.FlatAppearance.BorderSize = 0;
			this.copyPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.copyPathButton.Image = ((System.Drawing.Image)(resources.GetObject("copyPathButton.Image")));
			this.copyPathButton.Location = new System.Drawing.Point(342, 126);
			this.copyPathButton.Margin = new System.Windows.Forms.Padding(0);
			this.copyPathButton.Name = "copyPathButton";
			this.copyPathButton.Size = new System.Drawing.Size(32, 32);
			this.copyPathButton.TabIndex = 7;
			this.copyPathButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.copyPathButton.UseVisualStyleBackColor = false;
			this.copyPathButton.Click += new System.EventHandler(this.copyPathButton_Click);
			// 
			// copyFolderButton
			// 
			this.copyFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.copyFolderButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.copyFolderButton.FlatAppearance.BorderSize = 0;
			this.copyFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.copyFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("copyFolderButton.Image")));
			this.copyFolderButton.Location = new System.Drawing.Point(342, 84);
			this.copyFolderButton.Margin = new System.Windows.Forms.Padding(0);
			this.copyFolderButton.Name = "copyFolderButton";
			this.copyFolderButton.Size = new System.Drawing.Size(32, 32);
			this.copyFolderButton.TabIndex = 5;
			this.copyFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.copyFolderButton.UseVisualStyleBackColor = false;
			this.copyFolderButton.Click += new System.EventHandler(this.copyFolderButton_Click);
			// 
			// copyNameButton
			// 
			this.copyNameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.copyNameButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.copyNameButton.FlatAppearance.BorderSize = 0;
			this.copyNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.copyNameButton.Image = ((System.Drawing.Image)(resources.GetObject("copyNameButton.Image")));
			this.copyNameButton.Location = new System.Drawing.Point(342, 42);
			this.copyNameButton.Margin = new System.Windows.Forms.Padding(0);
			this.copyNameButton.Name = "copyNameButton";
			this.copyNameButton.Size = new System.Drawing.Size(32, 32);
			this.copyNameButton.TabIndex = 3;
			this.copyNameButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.copyNameButton.UseVisualStyleBackColor = false;
			this.copyNameButton.Click += new System.EventHandler(this.copyNameButton_Click);
			// 
			// compressionTextBox
			// 
			this.compressionTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.compressionTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.compressionTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.compressionTextBox.DarkMode = false;
			this.compressionTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.compressionTextBox.ForeColor = System.Drawing.Color.Black;
			this.compressionTextBox.Location = new System.Drawing.Point(122, 168);
			this.compressionTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.compressionTextBox.Name = "compressionTextBox";
			this.compressionTextBox.ReadOnly = true;
			this.compressionTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.compressionTextBox.Size = new System.Drawing.Size(70, 32);
			this.compressionTextBox.TabIndex = 8;
			this.compressionTextBox.WordWrap = true;
			// 
			// compressionLabel
			// 
			this.compressionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.compressionLabel.Location = new System.Drawing.Point(10, 174);
			this.compressionLabel.Margin = new System.Windows.Forms.Padding(0);
			this.compressionLabel.Name = "compressionLabel";
			this.compressionLabel.Size = new System.Drawing.Size(102, 19);
			this.compressionLabel.TabIndex = 33;
			this.compressionLabel.Text = "compression:";
			this.compressionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fullPathTextBox
			// 
			this.fullPathTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.fullPathTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.fullPathTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.fullPathTextBox.DarkMode = false;
			this.fullPathTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fullPathTextBox.ForeColor = System.Drawing.Color.Black;
			this.fullPathTextBox.Location = new System.Drawing.Point(122, 126);
			this.fullPathTextBox.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.fullPathTextBox.Name = "fullPathTextBox";
			this.fullPathTextBox.ReadOnly = true;
			this.fullPathTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fullPathTextBox.Size = new System.Drawing.Size(220, 32);
			this.fullPathTextBox.TabIndex = 6;
			this.fullPathTextBox.Text = "N/A";
			this.fullPathTextBox.WordWrap = false;
			// 
			// fullPathLabel
			// 
			this.fullPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fullPathLabel.Location = new System.Drawing.Point(10, 132);
			this.fullPathLabel.Margin = new System.Windows.Forms.Padding(0);
			this.fullPathLabel.Name = "fullPathLabel";
			this.fullPathLabel.Size = new System.Drawing.Size(102, 19);
			this.fullPathLabel.TabIndex = 31;
			this.fullPathLabel.Text = "full path:";
			this.fullPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// folderTextBox
			// 
			this.folderTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.folderTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.folderTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.folderTextBox.DarkMode = false;
			this.folderTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.folderTextBox.ForeColor = System.Drawing.Color.Black;
			this.folderTextBox.Location = new System.Drawing.Point(122, 84);
			this.folderTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.folderTextBox.Name = "folderTextBox";
			this.folderTextBox.ReadOnly = true;
			this.folderTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.folderTextBox.Size = new System.Drawing.Size(220, 32);
			this.folderTextBox.TabIndex = 4;
			this.folderTextBox.Text = "N/A";
			this.folderTextBox.WordWrap = true;
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.fileNameTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.fileNameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.fileNameTextBox.DarkMode = false;
			this.fileNameTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fileNameTextBox.ForeColor = System.Drawing.Color.Black;
			this.fileNameTextBox.Location = new System.Drawing.Point(122, 42);
			this.fileNameTextBox.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.ReadOnly = true;
			this.fileNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fileNameTextBox.Size = new System.Drawing.Size(220, 32);
			this.fileNameTextBox.TabIndex = 2;
			this.fileNameTextBox.Text = "N/A";
			this.fileNameTextBox.WordWrap = true;
			// 
			// folderLabel
			// 
			this.folderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.folderLabel.Location = new System.Drawing.Point(10, 90);
			this.folderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.folderLabel.Name = "folderLabel";
			this.folderLabel.Size = new System.Drawing.Size(102, 19);
			this.folderLabel.TabIndex = 28;
			this.folderLabel.Text = "folder:";
			this.folderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// sizeTextBox
			// 
			this.sizeTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.sizeTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.sizeTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.sizeTextBox.DarkMode = false;
			this.sizeTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.sizeTextBox.ForeColor = System.Drawing.Color.Black;
			this.sizeTextBox.Location = new System.Drawing.Point(521, 42);
			this.sizeTextBox.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.sizeTextBox.Name = "sizeTextBox";
			this.sizeTextBox.ReadOnly = true;
			this.sizeTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.sizeTextBox.Size = new System.Drawing.Size(199, 32);
			this.sizeTextBox.TabIndex = 13;
			this.sizeTextBox.WordWrap = true;
			// 
			// sizeLabel
			// 
			this.sizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sizeLabel.Location = new System.Drawing.Point(384, 48);
			this.sizeLabel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(127, 19);
			this.sizeLabel.TabIndex = 35;
			this.sizeLabel.Text = "size (px):";
			this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// megapixelsTextBox
			// 
			this.megapixelsTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.megapixelsTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.megapixelsTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.megapixelsTextBox.DarkMode = false;
			this.megapixelsTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.megapixelsTextBox.ForeColor = System.Drawing.Color.Black;
			this.megapixelsTextBox.Location = new System.Drawing.Point(521, 84);
			this.megapixelsTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.megapixelsTextBox.Name = "megapixelsTextBox";
			this.megapixelsTextBox.ReadOnly = true;
			this.megapixelsTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.megapixelsTextBox.Size = new System.Drawing.Size(199, 32);
			this.megapixelsTextBox.TabIndex = 14;
			this.megapixelsTextBox.WordWrap = true;
			// 
			// megapixelsLabel
			// 
			this.megapixelsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.megapixelsLabel.Location = new System.Drawing.Point(384, 90);
			this.megapixelsLabel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.megapixelsLabel.Name = "megapixelsLabel";
			this.megapixelsLabel.Size = new System.Drawing.Size(127, 19);
			this.megapixelsLabel.TabIndex = 37;
			this.megapixelsLabel.Text = "size (mp):";
			this.megapixelsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// resolutionTextBox
			// 
			this.resolutionTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.resolutionTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.resolutionTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.resolutionTextBox.DarkMode = false;
			this.resolutionTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.resolutionTextBox.ForeColor = System.Drawing.Color.Black;
			this.resolutionTextBox.Location = new System.Drawing.Point(521, 126);
			this.resolutionTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.resolutionTextBox.Name = "resolutionTextBox";
			this.resolutionTextBox.ReadOnly = true;
			this.resolutionTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.resolutionTextBox.Size = new System.Drawing.Size(199, 32);
			this.resolutionTextBox.TabIndex = 15;
			this.resolutionTextBox.WordWrap = true;
			// 
			// resolutionLabel
			// 
			this.resolutionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resolutionLabel.Location = new System.Drawing.Point(384, 132);
			this.resolutionLabel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.resolutionLabel.Name = "resolutionLabel";
			this.resolutionLabel.Size = new System.Drawing.Size(127, 19);
			this.resolutionLabel.TabIndex = 39;
			this.resolutionLabel.Text = "resolution:";
			this.resolutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// inchesTextBox
			// 
			this.inchesTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.inchesTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.inchesTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.inchesTextBox.DarkMode = false;
			this.inchesTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.inchesTextBox.ForeColor = System.Drawing.Color.Black;
			this.inchesTextBox.Location = new System.Drawing.Point(521, 168);
			this.inchesTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.inchesTextBox.Name = "inchesTextBox";
			this.inchesTextBox.ReadOnly = true;
			this.inchesTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.inchesTextBox.Size = new System.Drawing.Size(199, 32);
			this.inchesTextBox.TabIndex = 16;
			this.inchesTextBox.WordWrap = true;
			// 
			// inchesLabel
			// 
			this.inchesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inchesLabel.Location = new System.Drawing.Point(384, 174);
			this.inchesLabel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.inchesLabel.Name = "inchesLabel";
			this.inchesLabel.Size = new System.Drawing.Size(127, 19);
			this.inchesLabel.TabIndex = 41;
			this.inchesLabel.Text = "print size (in):";
			this.inchesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmTextBox
			// 
			this.cmTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.cmTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.cmTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.cmTextBox.DarkMode = false;
			this.cmTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cmTextBox.ForeColor = System.Drawing.Color.Black;
			this.cmTextBox.Location = new System.Drawing.Point(521, 210);
			this.cmTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.cmTextBox.Name = "cmTextBox";
			this.cmTextBox.ReadOnly = true;
			this.cmTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmTextBox.Size = new System.Drawing.Size(199, 32);
			this.cmTextBox.TabIndex = 17;
			this.cmTextBox.WordWrap = true;
			// 
			// cmLabel
			// 
			this.cmLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmLabel.Location = new System.Drawing.Point(384, 216);
			this.cmLabel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmLabel.Name = "cmLabel";
			this.cmLabel.Size = new System.Drawing.Size(127, 19);
			this.cmLabel.TabIndex = 43;
			this.cmLabel.Text = "print size (cm):";
			this.cmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ratioLabel
			// 
			this.ratioLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ratioLabel.Location = new System.Drawing.Point(384, 300);
			this.ratioLabel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.ratioLabel.Name = "ratioLabel";
			this.ratioLabel.Size = new System.Drawing.Size(127, 19);
			this.ratioLabel.TabIndex = 48;
			this.ratioLabel.Text = "aspect ratio:";
			this.ratioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ratioTextBox
			// 
			this.ratioTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ratioTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ratioTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ratioTextBox.DarkMode = false;
			this.ratioTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ratioTextBox.ForeColor = System.Drawing.Color.Black;
			this.ratioTextBox.Location = new System.Drawing.Point(521, 294);
			this.ratioTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.ratioTextBox.Name = "ratioTextBox";
			this.ratioTextBox.ReadOnly = true;
			this.ratioTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ratioTextBox.Size = new System.Drawing.Size(199, 32);
			this.ratioTextBox.TabIndex = 19;
			this.ratioTextBox.WordWrap = true;
			// 
			// diskSizeTextBox
			// 
			this.diskSizeTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.diskSizeTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.diskSizeTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.diskSizeTextBox.DarkMode = false;
			this.diskSizeTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.diskSizeTextBox.ForeColor = System.Drawing.Color.Black;
			this.diskSizeTextBox.Location = new System.Drawing.Point(521, 252);
			this.diskSizeTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.diskSizeTextBox.Name = "diskSizeTextBox";
			this.diskSizeTextBox.ReadOnly = true;
			this.diskSizeTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.diskSizeTextBox.Size = new System.Drawing.Size(199, 32);
			this.diskSizeTextBox.TabIndex = 18;
			this.diskSizeTextBox.Text = "N/A";
			this.diskSizeTextBox.WordWrap = true;
			// 
			// diskSizeLabel
			// 
			this.diskSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.diskSizeLabel.Location = new System.Drawing.Point(384, 257);
			this.diskSizeLabel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.diskSizeLabel.Name = "diskSizeLabel";
			this.diskSizeLabel.Size = new System.Drawing.Size(127, 19);
			this.diskSizeLabel.TabIndex = 45;
			this.diskSizeLabel.Text = "disk size:";
			this.diskSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// modifiedLabel
			// 
			this.modifiedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.modifiedLabel.Location = new System.Drawing.Point(10, 258);
			this.modifiedLabel.Margin = new System.Windows.Forms.Padding(0);
			this.modifiedLabel.Name = "modifiedLabel";
			this.modifiedLabel.Size = new System.Drawing.Size(102, 19);
			this.modifiedLabel.TabIndex = 39;
			this.modifiedLabel.Text = "modified:";
			this.modifiedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// modifiedTextBox
			// 
			this.modifiedTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.modifiedTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.modifiedTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.modifiedTextBox.DarkMode = false;
			this.modifiedTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.modifiedTextBox.ForeColor = System.Drawing.Color.Black;
			this.modifiedTextBox.Location = new System.Drawing.Point(122, 252);
			this.modifiedTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
			this.modifiedTextBox.Name = "modifiedTextBox";
			this.modifiedTextBox.ReadOnly = true;
			this.modifiedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.modifiedTextBox.Size = new System.Drawing.Size(252, 32);
			this.modifiedTextBox.TabIndex = 11;
			this.modifiedTextBox.Text = "N/A";
			this.modifiedTextBox.WordWrap = true;
			// 
			// createdLabel
			// 
			this.createdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.createdLabel.Location = new System.Drawing.Point(10, 216);
			this.createdLabel.Margin = new System.Windows.Forms.Padding(0);
			this.createdLabel.Name = "createdLabel";
			this.createdLabel.Size = new System.Drawing.Size(102, 19);
			this.createdLabel.TabIndex = 37;
			this.createdLabel.Text = "created:";
			this.createdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// createdTextBox
			// 
			this.createdTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.createdTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.createdTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.createdTextBox.DarkMode = false;
			this.createdTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.createdTextBox.ForeColor = System.Drawing.Color.Black;
			this.createdTextBox.Location = new System.Drawing.Point(122, 210);
			this.createdTextBox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.createdTextBox.Name = "createdTextBox";
			this.createdTextBox.ReadOnly = true;
			this.createdTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.createdTextBox.Size = new System.Drawing.Size(252, 32);
			this.createdTextBox.TabIndex = 10;
			this.createdTextBox.Text = "N/A";
			this.createdTextBox.WordWrap = true;
			// 
			// propertiesButton
			// 
			this.propertiesButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.propertiesButton.FlatAppearance.BorderSize = 0;
			this.propertiesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.propertiesButton.Image = ((System.Drawing.Image)(resources.GetObject("propertiesButton.Image")));
			this.propertiesButton.Location = new System.Drawing.Point(122, 294);
			this.propertiesButton.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.propertiesButton.Name = "propertiesButton";
			this.propertiesButton.Size = new System.Drawing.Size(252, 32);
			this.propertiesButton.TabIndex = 12;
			this.propertiesButton.Text = " File properties";
			this.propertiesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.propertiesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.propertiesButton.UseVisualStyleBackColor = false;
			this.propertiesButton.Visible = false;
			this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
			// 
			// titlePanel
			// 
			this.titlePanel.Controls.Add(this.titleLabel);
			this.titlePanel.Controls.Add(this.closeBtn);
			this.titlePanel.Location = new System.Drawing.Point(0, 0);
			this.titlePanel.Margin = new System.Windows.Forms.Padding(0);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new System.Drawing.Size(730, 32);
			this.titlePanel.TabIndex = 0;
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Location = new System.Drawing.Point(10, 7);
			this.titleLabel.Margin = new System.Windows.Forms.Padding(0, 9, 0, 9);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(65, 19);
			this.titleLabel.TabIndex = 16;
			this.titleLabel.Text = "InfoForm";
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
			this.closeBtn.Location = new System.Drawing.Point(698, 0);
			this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(32, 32);
			this.closeBtn.TabIndex = 1;
			this.closeBtn.UseVisualStyleBackColor = true;
			// 
			// InfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.ClientSize = new System.Drawing.Size(730, 336);
			this.CloseButton = this.closeBtn;
			this.Controls.Add(this.modifiedLabel);
			this.Controls.Add(this.copyPathButton);
			this.Controls.Add(this.modifiedTextBox);
			this.Controls.Add(this.ratioLabel);
			this.Controls.Add(this.createdLabel);
			this.Controls.Add(this.copyFolderButton);
			this.Controls.Add(this.createdTextBox);
			this.Controls.Add(this.extensionLabel);
			this.Controls.Add(this.copyNameButton);
			this.Controls.Add(this.ratioTextBox);
			this.Controls.Add(this.extensionTextBox);
			this.Controls.Add(this.diskSizeLabel);
			this.Controls.Add(this.diskSizeTextBox);
			this.Controls.Add(this.titlePanel);
			this.Controls.Add(this.propertiesButton);
			this.Controls.Add(this.cmTextBox);
			this.Controls.Add(this.inchesTextBox);
			this.Controls.Add(this.sizeTextBox);
			this.Controls.Add(this.resolutionTextBox);
			this.Controls.Add(this.megapixelsTextBox);
			this.Controls.Add(this.sizeLabel);
			this.Controls.Add(this.cmLabel);
			this.Controls.Add(this.inchesLabel);
			this.Controls.Add(this.megapixelsLabel);
			this.Controls.Add(this.resolutionLabel);
			this.Controls.Add(this.compressionTextBox);
			this.Controls.Add(this.fullPathLabel);
			this.Controls.Add(this.fullPathTextBox);
			this.Controls.Add(this.compressionLabel);
			this.Controls.Add(this.fileNameLabel);
			this.Controls.Add(this.folderLabel);
			this.Controls.Add(this.folderTextBox);
			this.Controls.Add(this.fileNameTextBox);
			this.Draggable = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InfoForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InfoForm";
			this.TitleLabel = this.titleLabel;
			this.Load += new System.EventHandler(this.InfoForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InfoForm_KeyDown);
			this.titlePanel.ResumeLayout(false);
			this.titlePanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label fileNameLabel;
		private System.Windows.Forms.Label folderLabel;
		private QuickLibrary.QlibTextBox fileNameTextBox;
		private QuickLibrary.QlibTextBox folderTextBox;
		private QuickLibrary.QlibTextBox fullPathTextBox;
		private System.Windows.Forms.Label fullPathLabel;
		private QuickLibrary.QlibTextBox compressionTextBox;
		private System.Windows.Forms.Label compressionLabel;
		private QuickLibrary.QlibTextBox sizeTextBox;
		private System.Windows.Forms.Label sizeLabel;
		private QuickLibrary.QlibTextBox megapixelsTextBox;
		private System.Windows.Forms.Label megapixelsLabel;
		private QuickLibrary.QlibTextBox resolutionTextBox;
		private System.Windows.Forms.Label resolutionLabel;
		private QuickLibrary.QlibTextBox inchesTextBox;
		private System.Windows.Forms.Label inchesLabel;
		private QuickLibrary.QlibTextBox cmTextBox;
		private System.Windows.Forms.Label cmLabel;
		private System.Windows.Forms.Label createdLabel;
		private QuickLibrary.QlibTextBox createdTextBox;
		private System.Windows.Forms.Label modifiedLabel;
		private QuickLibrary.QlibTextBox modifiedTextBox;
		private QuickLibrary.QlibTextBox diskSizeTextBox;
		private System.Windows.Forms.Label diskSizeLabel;
		private System.Windows.Forms.Button propertiesButton;
		private System.Windows.Forms.Label ratioLabel;
		private QuickLibrary.QlibTextBox ratioTextBox;
		private System.Windows.Forms.Button copyNameButton;
		private System.Windows.Forms.Button copyFolderButton;
		private System.Windows.Forms.Button copyPathButton;
		private QuickLibrary.QlibTextBox extensionTextBox;
		private System.Windows.Forms.Label extensionLabel;
		private System.Windows.Forms.ToolTip infoTooltip;
		private System.Windows.Forms.Panel titlePanel;
		private System.Windows.Forms.Label titleLabel;
		private QuickLibrary.QlibTitlebarButton closeBtn;
	}
}
