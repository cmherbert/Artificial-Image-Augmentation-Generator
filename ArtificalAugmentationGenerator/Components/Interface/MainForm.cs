using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtificalAugmentationGenerator.Components.Options;
using ArtificalAugmentationGenerator.Components.Interface.Dialogs;
using ArtificalAugmentationGenerator.Components.Interface.CIR;
using System.IO;

namespace ArtificalAugmentationGenerator.Components.Interface
{
    internal partial class MainForm : Form
    {
        readonly ResourceController _resourceController;
        private int childFormNumber = 0;
        readonly IAGOptions _options;
        public MainForm(IAGOptions options)
        {
            _options = options;
            InitializeComponent();
            _resourceController = new ResourceController(this);
        }

        private void XUI_TB_NW_FSD_Click(object sender, EventArgs e)
        {
            Form childForm = new XD_FileSource(_resourceController.DialogController);
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void XUI_TB_NW_PD_Click(object sender, EventArgs e)
        {
            XD_Performance perf = new XD_Performance();
            perf.MdiParent = this;
            perf.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void restorePreviousSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("quickrestore.txt"))
            {
                var files = File.ReadAllLines("quickrestore.txt");
                Form childForm = new XD_FileSource(_resourceController.DialogController, files);
                childForm.MdiParent = this;
                //childForm.Text = "Window " + childFormNumber++;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Nothing to restore");
            }
        }
    }
}
