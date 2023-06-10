namespace ArtificalAugmentationGenerator.Components.Interface.Controls
{
    partial class LabelledTextbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ui_label = new System.Windows.Forms.Label();
            this.ui_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ui_label
            // 
            this.ui_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.ui_label.Location = new System.Drawing.Point(0, 0);
            this.ui_label.Name = "ui_label";
            this.ui_label.Size = new System.Drawing.Size(41, 21);
            this.ui_label.TabIndex = 0;
            this.ui_label.Text = "label1";
            this.ui_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ui_text
            // 
            this.ui_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_text.Location = new System.Drawing.Point(41, 0);
            this.ui_text.Name = "ui_text";
            this.ui_text.Size = new System.Drawing.Size(367, 20);
            this.ui_text.TabIndex = 1;
            // 
            // LabelledTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ui_text);
            this.Controls.Add(this.ui_label);
            this.Name = "LabelledTextbox";
            this.Size = new System.Drawing.Size(408, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ui_label;
        private System.Windows.Forms.TextBox ui_text;
    }
}
