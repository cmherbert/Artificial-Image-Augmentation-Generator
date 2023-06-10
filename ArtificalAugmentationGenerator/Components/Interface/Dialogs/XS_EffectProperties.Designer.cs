namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    partial class XS_EffectProperties
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.xui_imagecount = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xui_targetid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledTextbox();
            this.xui_sourceid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledTextbox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.xui_effectname = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.xui_props = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_imagecount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 363);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(303, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // xui_imagecount
            // 
            this.xui_imagecount.Name = "xui_imagecount";
            this.xui_imagecount.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xui_targetid);
            this.groupBox1.Controls.Add(this.xui_sourceid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(303, 76);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // xui_targetid
            // 
            this.xui_targetid.Dock = System.Windows.Forms.DockStyle.Top;
            this.xui_targetid.LabelWidth = 48;
            this.xui_targetid.Location = new System.Drawing.Point(10, 44);
            this.xui_targetid.Name = "xui_targetid";
            this.xui_targetid.Size = new System.Drawing.Size(283, 21);
            this.xui_targetid.TabIndex = 1;
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
            this.toolStripButton1,
            this.xui_effectname,
            this.toolStripSeparator1,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(303, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ArtificalAugmentationGenerator.Properties.Resources.OK;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "OK";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // xui_effectname
            // 
            this.xui_effectname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xui_effectname.Name = "xui_effectname";
            this.xui_effectname.Size = new System.Drawing.Size(77, 22);
            this.xui_effectname.Text = "Effect Name";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::ArtificalAugmentationGenerator.Properties.Resources._112_RefreshArrow_Blue_16x16_72;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Reset properties to default";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // xui_props
            // 
            this.xui_props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xui_props.Location = new System.Drawing.Point(0, 101);
            this.xui_props.Name = "xui_props";
            this.xui_props.Size = new System.Drawing.Size(303, 262);
            this.xui_props.TabIndex = 16;
            // 
            // XS_EffectProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 385);
            this.Controls.Add(this.xui_props);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(319, 424);
            this.MinimumSize = new System.Drawing.Size(319, 424);
            this.Name = "XS_EffectProperties";
            this.Text = "Effect Properties";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel xui_imagecount;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.LabelledTextbox xui_sourceid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Controls.LabelledTextbox xui_targetid;
        private System.Windows.Forms.PropertyGrid xui_props;
        private System.Windows.Forms.ToolStripLabel xui_effectname;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}