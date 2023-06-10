namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    partial class XD_Preview
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
            this.xui_imagesource = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xui_targetid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledTextbox();
            this.xui_sourceid = new ArtificalAugmentationGenerator.Components.Interface.Controls.LabelledTextbox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xui_effectname = new System.Windows.Forms.ToolStripLabel();
            this.saveImage = new System.Windows.Forms.ToolStripButton();
            this.copyToClip = new System.Windows.Forms.ToolStripButton();
            this.xui_refresh = new System.Windows.Forms.ToolStripButton();
            this.xui_image = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xui_image)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_imagecount,
            this.xui_imagesource});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // xui_imagecount
            // 
            this.xui_imagecount.Name = "xui_imagecount";
            this.xui_imagecount.Size = new System.Drawing.Size(0, 17);
            // 
            // xui_imagesource
            // 
            this.xui_imagesource.Name = "xui_imagesource";
            this.xui_imagesource.Size = new System.Drawing.Size(74, 17);
            this.xui_imagesource.Text = "xui_sourceid";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xui_targetid);
            this.groupBox1.Controls.Add(this.xui_sourceid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(800, 76);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // xui_targetid
            // 
            this.xui_targetid.Dock = System.Windows.Forms.DockStyle.Top;
            this.xui_targetid.LabelWidth = 48;
            this.xui_targetid.Location = new System.Drawing.Point(10, 44);
            this.xui_targetid.Name = "xui_targetid";
            this.xui_targetid.Size = new System.Drawing.Size(780, 21);
            this.xui_targetid.TabIndex = 1;
            // 
            // xui_sourceid
            // 
            this.xui_sourceid.Dock = System.Windows.Forms.DockStyle.Top;
            this.xui_sourceid.LabelWidth = 48;
            this.xui_sourceid.Location = new System.Drawing.Point(10, 23);
            this.xui_sourceid.Name = "xui_sourceid";
            this.xui_sourceid.Size = new System.Drawing.Size(780, 21);
            this.xui_sourceid.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xui_effectname,
            this.saveImage,
            this.copyToClip,
            this.xui_refresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xui_effectname
            // 
            this.xui_effectname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xui_effectname.Name = "xui_effectname";
            this.xui_effectname.Size = new System.Drawing.Size(96, 22);
            this.xui_effectname.Text = "Effect Name(s)";
            // 
            // saveImage
            // 
            this.saveImage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveImage.Image = global::ArtificalAugmentationGenerator.Properties.Resources.saveHS;
            this.saveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveImage.Name = "saveImage";
            this.saveImage.Size = new System.Drawing.Size(23, 22);
            this.saveImage.Text = "Save";
            this.saveImage.Click += new System.EventHandler(this.saveImage_Click);
            // 
            // copyToClip
            // 
            this.copyToClip.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.copyToClip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToClip.Image = global::ArtificalAugmentationGenerator.Properties.Resources.CopyHS;
            this.copyToClip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToClip.Name = "copyToClip";
            this.copyToClip.Size = new System.Drawing.Size(23, 22);
            this.copyToClip.Text = "Copy to Clipboard";
            this.copyToClip.Click += new System.EventHandler(this.copyToClip_Click);
            // 
            // xui_refresh
            // 
            this.xui_refresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xui_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xui_refresh.Image = global::ArtificalAugmentationGenerator.Properties.Resources._112_RefreshArrow_Blue_16x16_72;
            this.xui_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xui_refresh.Name = "xui_refresh";
            this.xui_refresh.Size = new System.Drawing.Size(23, 22);
            this.xui_refresh.Text = "Regenerate Augmentation";
            this.xui_refresh.Click += new System.EventHandler(this.xui_refresh_Click);
            // 
            // xui_image
            // 
            this.xui_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xui_image.Location = new System.Drawing.Point(0, 101);
            this.xui_image.Name = "xui_image";
            this.xui_image.Size = new System.Drawing.Size(800, 327);
            this.xui_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xui_image.TabIndex = 19;
            this.xui_image.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG File|*.png|JPEG File|*.jpg;*.jpeg";
            // 
            // XD_Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.xui_image);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "XD_Preview";
            this.Text = "Preview Window";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xui_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel xui_imagecount;
        private System.Windows.Forms.ToolStripStatusLabel xui_imagesource;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.LabelledTextbox xui_targetid;
        private Controls.LabelledTextbox xui_sourceid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel xui_effectname;
        private System.Windows.Forms.PictureBox xui_image;
        private System.Windows.Forms.ToolStripButton copyToClip;
        private System.Windows.Forms.ToolStripButton xui_refresh;
        private System.Windows.Forms.ToolStripButton saveImage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}