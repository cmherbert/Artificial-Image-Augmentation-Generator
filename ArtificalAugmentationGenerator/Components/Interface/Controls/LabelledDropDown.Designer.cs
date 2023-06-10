namespace ArtificalAugmentationGenerator.Components.Interface.Controls
{
    partial class LabelledDropDown
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
            this.ui_dropdown = new System.Windows.Forms.ComboBox();
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
            // ui_dropdown
            // 
            this.ui_dropdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_dropdown.FormattingEnabled = true;
            this.ui_dropdown.Location = new System.Drawing.Point(41, 0);
            this.ui_dropdown.Name = "ui_dropdown";
            this.ui_dropdown.Size = new System.Drawing.Size(367, 21);
            this.ui_dropdown.TabIndex = 1;
            // 
            // LabeledDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ui_dropdown);
            this.Controls.Add(this.ui_label);
            this.Name = "LabeledDropDown";
            this.Size = new System.Drawing.Size(408, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ui_label;
        private System.Windows.Forms.ComboBox ui_dropdown;
    }
}
