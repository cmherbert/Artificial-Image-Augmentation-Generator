namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    partial class XD_AugmentationSelection
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.xui_imagecount = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xui_fssourceid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledDropDown();
            this.xui_sourceid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledTextbox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xui_runone = new System.Windows.Forms.ToolStripButton();
            this.xui_runall = new System.Windows.Forms.ToolStripButton();
            this.xui_effectlist = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xui_effectedit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_imagecount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(303, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // xui_imagecount
            // 
            this.xui_imagecount.Name = "xui_imagecount";
            this.xui_imagecount.Size = new System.Drawing.Size(56, 17);
            this.xui_imagecount.Text = "0 Effects";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xui_fssourceid);
            this.groupBox1.Controls.Add(this.xui_sourceid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(303, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // xui_fssourceid
            // 
            this.xui_fssourceid.Dock = System.Windows.Forms.DockStyle.Top;
            this.xui_fssourceid.LabelWidth = 48;
            this.xui_fssourceid.Location = new System.Drawing.Point(10, 44);
            this.xui_fssourceid.Name = "xui_fssourceid";
            this.xui_fssourceid.Size = new System.Drawing.Size(283, 21);
            this.xui_fssourceid.TabIndex = 1;
            // 
            // xui_sourceid
            // 
            this.xui_sourceid.Dock = System.Windows.Forms.DockStyle.Top;
            this.xui_sourceid.LabelWidth = 48;
            this.xui_sourceid.Location = new System.Drawing.Point(10, 23);
            this.xui_sourceid.Name = "xui_sourceid";
            this.xui_sourceid.Size = new System.Drawing.Size(283, 21);
            this.xui_sourceid.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_runone,
            this.xui_runall});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(303, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xui_runone
            // 
            this.xui_runone.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xui_runone.Image = global::ArtificalAugmentationGenerator.Properties.Resources.BuilderDialog_add;
            this.xui_runone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xui_runone.Name = "xui_runone";
            this.xui_runone.Size = new System.Drawing.Size(48, 22);
            this.xui_runone.Text = "&Run";
            this.xui_runone.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.xui_runone.Click += new System.EventHandler(this.xui_runone_Click);
            // 
            // xui_runall
            // 
            this.xui_runall.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xui_runall.Enabled = false;
            this.xui_runall.Image = global::ArtificalAugmentationGenerator.Properties.Resources.BuilderDialog_AddAll;
            this.xui_runall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xui_runall.Name = "xui_runall";
            this.xui_runall.Size = new System.Drawing.Size(64, 22);
            this.xui_runall.Text = "Run &All";
            this.xui_runall.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.xui_runall.Click += new System.EventHandler(this.xui_runall_Click);
            // 
            // xui_effectlist
            // 
            this.xui_effectlist.CheckOnClick = true;
            this.xui_effectlist.ContextMenuStrip = this.contextMenuStrip1;
            this.xui_effectlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xui_effectlist.FormattingEnabled = true;
            this.xui_effectlist.Location = new System.Drawing.Point(0, 101);
            this.xui_effectlist.Name = "xui_effectlist";
            this.xui_effectlist.Size = new System.Drawing.Size(303, 264);
            this.xui_effectlist.TabIndex = 12;
            this.xui_effectlist.ThreeDCheckBoxes = true;
            this.xui_effectlist.DoubleClick += new System.EventHandler(this.xui_effectlist_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_effectedit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // xui_effectedit
            // 
            this.xui_effectedit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xui_effectedit.Name = "xui_effectedit";
            this.xui_effectedit.Size = new System.Drawing.Size(165, 22);
            this.xui_effectedit.Text = "Edit &Properties";
            this.xui_effectedit.Click += new System.EventHandler(this.xui_effectedit_Click);
            // 
            // XD_AugmentationSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 387);
            this.Controls.Add(this.xui_effectlist);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(319, 424);
            this.MinimumSize = new System.Drawing.Size(319, 424);
            this.Name = "XD_AugmentationSelection";
            this.Text = "Augmentation Selection";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel xui_imagecount;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.LabelledDropDown xui_fssourceid;
        private Controls.LabelledTextbox xui_sourceid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckedListBox xui_effectlist;
        private System.Windows.Forms.ToolStripButton xui_runone;
        private System.Windows.Forms.ToolStripButton xui_runall;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xui_effectedit;
    }
}