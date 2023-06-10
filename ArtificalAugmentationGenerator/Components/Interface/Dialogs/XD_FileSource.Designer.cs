namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    partial class XD_FileSource
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XD_FileSource));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.xui_imagecount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xui_open = new System.Windows.Forms.ToolStripSplitButton();
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.xui_selectnone = new System.Windows.Forms.ToolStripMenuItem();
            this.xui_selectall = new System.Windows.Forms.ToolStripMenuItem();
            this.xui_invertselect = new System.Windows.Forms.ToolStripMenuItem();
            this.xui_selectfx = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xui_createfilter = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xui_files = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xui_sourceid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledTextbox();
            this.xdiag_openfolder = new System.Windows.Forms.FolderBrowserDialog();
            this.xdiag_openfiles = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_imagecount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 479);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(381, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // xui_imagecount
            // 
            this.xui_imagecount.Name = "xui_imagecount";
            this.xui_imagecount.Size = new System.Drawing.Size(57, 17);
            this.xui_imagecount.Text = "0 Images";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_open,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.xui_selectfx,
            this.toolStripSeparator3,
            this.xui_createfilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(381, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // xui_open
            // 
            this.xui_open.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilesToolStripMenuItem});
            this.xui_open.Image = ((System.Drawing.Image)(resources.GetObject("xui_open.Image")));
            this.xui_open.ImageTransparentColor = System.Drawing.Color.Black;
            this.xui_open.Name = "xui_open";
            this.xui_open.Size = new System.Drawing.Size(118, 22);
            this.xui_open.Text = "Open Folder...";
            this.xui_open.ButtonClick += new System.EventHandler(this.xui_open_ButtonClick);
            this.xui_open.Click += new System.EventHandler(this.xui_open_Click);
            // 
            // openFilesToolStripMenuItem
            // 
            this.openFilesToolStripMenuItem.Image = global::ArtificalAugmentationGenerator.Properties.Resources.Open;
            this.openFilesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.openFilesToolStripMenuItem.Text = "Open Files...";
            this.openFilesToolStripMenuItem.Click += new System.EventHandler(this.openFilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_selectnone,
            this.xui_selectall,
            this.xui_invertselect});
            this.toolStripButton1.Image = global::ArtificalAugmentationGenerator.Properties.Resources.CheckBoxHS;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton1.Text = "Quick Select";
            this.toolStripButton1.ButtonClick += new System.EventHandler(this.xui_invertselect_Click);
            // 
            // xui_selectnone
            // 
            this.xui_selectnone.Name = "xui_selectnone";
            this.xui_selectnone.Size = new System.Drawing.Size(161, 22);
            this.xui_selectnone.Text = "Select &None";
            this.xui_selectnone.Click += new System.EventHandler(this.selectNoneToolStripMenuItem_Click);
            // 
            // xui_selectall
            // 
            this.xui_selectall.Name = "xui_selectall";
            this.xui_selectall.Size = new System.Drawing.Size(161, 22);
            this.xui_selectall.Text = "Select &All";
            this.xui_selectall.Click += new System.EventHandler(this.xui_selectall_Click);
            // 
            // xui_invertselect
            // 
            this.xui_invertselect.Name = "xui_invertselect";
            this.xui_invertselect.Size = new System.Drawing.Size(161, 22);
            this.xui_invertselect.Text = "&Invert Selection";
            this.xui_invertselect.Click += new System.EventHandler(this.xui_invertselect_Click);
            // 
            // xui_selectfx
            // 
            this.xui_selectfx.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xui_selectfx.Image = global::ArtificalAugmentationGenerator.Properties.Resources.BuilderDialog_add;
            this.xui_selectfx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xui_selectfx.Name = "xui_selectfx";
            this.xui_selectfx.Size = new System.Drawing.Size(65, 22);
            this.xui_selectfx.Text = "&Effects";
            this.xui_selectfx.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.xui_selectfx.Click += new System.EventHandler(this.xui_selectfx_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // xui_createfilter
            // 
            this.xui_createfilter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xui_createfilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xui_createfilter.Enabled = false;
            this.xui_createfilter.Image = global::ArtificalAugmentationGenerator.Properties.Resources.Filter2HS;
            this.xui_createfilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xui_createfilter.Name = "xui_createfilter";
            this.xui_createfilter.Size = new System.Drawing.Size(23, 22);
            this.xui_createfilter.Text = "Create Filter";
            this.xui_createfilter.Click += new System.EventHandler(this.xui_createfilter_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xui_files);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(381, 454);
            this.panel1.TabIndex = 4;
            // 
            // xui_files
            // 
            this.xui_files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.xui_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xui_files.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.xui_files.HideSelection = false;
            this.xui_files.Location = new System.Drawing.Point(3, 63);
            this.xui_files.Name = "xui_files";
            this.xui_files.Size = new System.Drawing.Size(375, 388);
            this.xui_files.TabIndex = 3;
            this.xui_files.UseCompatibleStateImageBehavior = false;
            this.xui_files.View = System.Windows.Forms.View.Details;
            this.xui_files.SelectedIndexChanged += new System.EventHandler(this.xui_files_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xui_sourceid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(375, 60);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // xui_sourceid
            // 
            this.xui_sourceid.Dock = System.Windows.Forms.DockStyle.Top;
            this.xui_sourceid.LabelWidth = 48;
            this.xui_sourceid.Location = new System.Drawing.Point(10, 23);
            this.xui_sourceid.Name = "xui_sourceid";
            this.xui_sourceid.Size = new System.Drawing.Size(355, 21);
            this.xui_sourceid.TabIndex = 0;
            // 
            // xdiag_openfolder
            // 
            this.xdiag_openfolder.RootFolder = System.Environment.SpecialFolder.UserProfile;
            // 
            // xdiag_openfiles
            // 
            this.xdiag_openfiles.FileName = "Select Files";
            this.xdiag_openfiles.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.png;*.tiff";
            this.xdiag_openfiles.Multiselect = true;
            // 
            // XD_FileSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 501);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "XD_FileSource";
            this.Text = "Source Images";
            this.Load += new System.EventHandler(this.XDIAG_SourceSelector_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView xui_files;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.LabelledTextbox xui_sourceid;
        private System.Windows.Forms.ToolStripStatusLabel xui_imagecount;
        private System.Windows.Forms.FolderBrowserDialog xdiag_openfolder;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog xdiag_openfiles;
        public System.Windows.Forms.ToolStripSplitButton xui_open;
        private System.Windows.Forms.ToolStripMenuItem openFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem xui_selectnone;
        private System.Windows.Forms.ToolStripMenuItem xui_selectall;
        private System.Windows.Forms.ToolStripMenuItem xui_invertselect;
        private System.Windows.Forms.ToolStripButton xui_createfilter;
        private System.Windows.Forms.ToolStripButton xui_selectfx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}