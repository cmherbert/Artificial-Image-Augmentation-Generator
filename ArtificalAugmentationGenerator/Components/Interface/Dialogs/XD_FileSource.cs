using ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    internal partial class XD_FileSource : Form, IDialog_FileSource, IDialog_FileSelection
    {
        private SGUID _id = SGUID.NewSGUID();
        private readonly DialogController _dialogController;
        internal List<string> _sourceFiles = new List<string>();
        public IReadOnlyList<string> SourceFiles => _sourceFiles.AsReadOnly();
        public IReadOnlyList<string> SelectedFiles => xui_files.SelectedItems.Cast<ListViewItem>().Select(x => x.SubItems[0].Text).ToList().AsReadOnly();

        public event EventHandler SourceFilesChanged;
        public event EventHandler SelectedFilesChanged;

        public SGUID DialogID => _id;

        public XD_FileSource(DialogController dialogController, string[] files = null)
        {
            InitializeComponent();
            xui_sourceid.Label.Text = "ID:";
            _dialogController = dialogController;
            xui_sourceid.Textbox.ReadOnly = true;
            xui_sourceid.Textbox.Text = _id.ToString();

            if (files != null)
                AppendFiles(files.ToList(), true);
        }


        private void xui_open_Click(object sender, EventArgs e)
        {

        }

        private void XDIAG_SourceSelector_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void xui_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedFilesChanged?.GetInvocationList().ToList().ForEach(x => x.DynamicInvoke(this, null));
        }

        private void xui_open_ButtonClick(object sender, EventArgs e)
        {
            if (xdiag_openfolder.ShowDialog() == DialogResult.OK)
            {

                string fpath = xdiag_openfolder.SelectedPath;
                List<string> files = (List<string>)Idle.TaskWorker.ShowWorkerAsync(this, () => Directory.EnumerateFiles(fpath, "*", MessageBox.Show("Do you want to search top directory only?", "Search Level", MessageBoxButtons.YesNo) == DialogResult.No ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Where(x => x.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || x.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase) || x.EndsWith(".gif", StringComparison.InvariantCultureIgnoreCase) || x.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase)).ToList());

                AppendFiles(files);

                //_sourceFiles.Clear();
                //xui_files.Items.Clear();
                //_sourceFiles = files;
                //xui_files.SuspendLayout();
                //xui_files.Items.AddRange(items);
                //xui_files.ResumeLayout();
                //xui_files.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //xui_files.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                //xui_imagecount.Text = $"{xui_files.Items.Count} Images";
                //SourceFilesChanged?.GetInvocationList().ToList().ForEach(x => x.DynamicInvoke(this, null));

            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xdiag_openfiles.ShowDialog() == DialogResult.OK)
            {
                AppendFiles(xdiag_openfiles.FileNames.ToList());
            }
        }
        private void AppendFiles(List<string> files, bool forceReplace = false)
        {
            xui_files.SuspendLayout();
            if (_sourceFiles.Count == 0 || forceReplace || MessageBox.Show(this, "Do you want to append images to the existing collection?", "Existing data warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                _sourceFiles.Clear();
                xui_files.Items.Clear();
                _sourceFiles = files;
                ListViewItem[] items = (ListViewItem[])Idle.TaskWorker.ShowWorkerAsync(this, () => files.AsParallel().Select(x => new ListViewItem(x)).ToArray());
                xui_files.Items.AddRange(items);
            }
            else
            {
                files = files.Where(x => !_sourceFiles.Contains(x)).ToList();
                ListViewItem[] items = (ListViewItem[])Idle.TaskWorker.ShowWorkerAsync(this, () => files.AsParallel().Select(x => new ListViewItem(x)).ToArray());
                xui_files.Items.AddRange(items);


            }
            xui_files.ResumeLayout();
            xui_files.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            xui_files.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            xui_imagecount.Text = $"{xui_files.Items.Count} Images";
            SourceFilesChanged?.GetInvocationList().ToList().ForEach(x => x.DynamicInvoke(this, null));

            try
            {
                //Write last file list
                using (var output = File.Create("quickrestore.txt"))
                using (var sw = new StreamWriter(output))
                {
                    foreach (var file in SourceFiles)
                        sw.WriteLine(file);
                }
            }
            catch { }
        }

        private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in xui_files.Items)
                item.Selected = false;
        }

        private void xui_invertselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in xui_files.Items)
                item.Selected = !item.Selected;
        }

        private void xui_selectall_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in xui_files.Items)
                item.Selected = true;
        }

        private void xui_createfilter_Click(object sender, EventArgs e)
        {
            //_dialogController.CreateChild(new XD_FileSelection(_dialogController, DialogID));
        }

        private void xui_selectfx_Click(object sender, EventArgs e)
        {
            _dialogController.CreateChild<XD_AugmentationSelection>(new XD_AugmentationSelection(_dialogController, DialogID));
        }
    }
}
