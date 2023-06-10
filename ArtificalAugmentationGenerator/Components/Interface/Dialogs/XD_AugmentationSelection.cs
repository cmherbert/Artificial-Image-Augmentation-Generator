using ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces;
using ArtificalAugmentationGenerator.Plugins;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    internal partial class XD_AugmentationSelection : Form, IDialog_EffectSelection
    {
        private SGUID _id = SGUID.NewSGUID();
        private readonly DialogController _dialogController;
        private IDialog_FileSelection _target = null;

        public Augmentation SelectedEffect => throw new NotImplementedException();
        public SGUID DialogID => _id;
        public XD_AugmentationSelection(DialogController dialogController)
        {
            InitializeComponent();
            _dialogController = dialogController;
            Initialise();
        }
        public XD_AugmentationSelection(DialogController dialogController, SGUID target)
        {
            InitializeComponent();
            _dialogController = dialogController;
            Initialise();
            xui_fssourceid.Dropdown.SelectedItem = target;


        }

        private void Initialise()
        {

            //_dialogController.MdiEvent += DiialogController_MdiEvent;
            xui_fssourceid.Dropdown.SelectedIndexChanged += Dropdown_SelectedIndexChanged;
            xui_sourceid.Label.Text = "ID:";
            xui_fssourceid.Label.Text = "Target:";
            xui_sourceid.Textbox.Text = DialogID.ToString();
            xui_sourceid.Textbox.ReadOnly = true;
            UpdateFileSelectionTargetComboBox();
            UpdateCheckList();
        }



        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //_dialogController.MdiEvent -= DiialogController_MdiEvent;

        }


        private void DiialogController_MdiEvent(object sender, EventArgs e)
        {
            UpdateFileSelectionTargetComboBox();
        }

        private void UpdateFileSelectionTargetComboBox()
        {
            if (xui_fssourceid.Dropdown.Items.Count == 0)
            {
                xui_fssourceid.Dropdown.Items.Add(SGUID.Empty);
                xui_fssourceid.Dropdown.SelectedItem = SGUID.Empty;
            }
            var targets = _dialogController.FindAllChildren<IDialog_FileSelection>().Select(x => x.DialogID).ToList();
            var target = xui_fssourceid.Dropdown.SelectedItem;

            //Calculate to be cleared and added
            List<SGUID> toDelete = xui_fssourceid.Dropdown.Items.Cast<SGUID>().Where(x => !targets.Contains(x)).Where(x => x != SGUID.Empty).ToList();
            List<SGUID> toAdd = targets.Where(x => !xui_fssourceid.Dropdown.Items.Contains(x)).ToList();

            //If target is in toDelete, set null
            if (toDelete.Contains(xui_fssourceid.Dropdown.SelectedItem))
                xui_fssourceid.Dropdown.SelectedItem = SGUID.Empty;

            toDelete.ForEach(x => xui_fssourceid.Dropdown.Items.Remove(x));
            toAdd.ForEach(x => xui_fssourceid.Dropdown.Items.Add(x));

        }



        private void Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SGUID target = (SGUID)xui_fssourceid.Dropdown.SelectedItem;
            _target = _dialogController.FindChild<IDialog_FileSelection>(target);
        }

        private void UpdateCheckList()
        {
            xui_effectlist.Items.Clear();
            foreach (var effect in ContentManager.Augmentations)
            {
                xui_effectlist.Items.Add(new AugmentationEntry((Augmentation)Activator.CreateInstance(effect.Item.GetType())));
            }
            xui_imagecount.Text = $"{xui_effectlist.Items.Count} Augmentations";
        }

        internal struct AugmentationEntry
        {
            Augmentation _effect;
            public Augmentation Augmentation => _effect;
            public AugmentationEntry(Augmentation entry)
            {
                _effect = entry;
            }
            public override string ToString()
            {
                return _effect.Name;
            }
        }

        private void xui_effectlist_DoubleClick(object sender, EventArgs e)
        {
            if (xui_effectlist.SelectedItem != null)
            {
                _dialogController.CreateChild<XS_EffectProperties>(new XS_EffectProperties(_dialogController, DialogID, ((AugmentationEntry)xui_effectlist.SelectedItem).Augmentation));
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (xui_effectlist.SelectedItem == null)
                e.Cancel = true;
        }

        private void xui_effectedit_Click(object sender, EventArgs e)
        {
            if (xui_effectlist.SelectedItem != null)
            {
                _dialogController.CreateChild<XS_EffectProperties>(new XS_EffectProperties(_dialogController, DialogID, ((AugmentationEntry)xui_effectlist.SelectedItem).Augmentation));
            }
        }

        private void xui_effectinfo_Click(object sender, EventArgs e)
        {

        }

        private void xui_checkeffect_Click(object sender, EventArgs e)
        {

        }

        private void xui_runone_Click(object sender, EventArgs e)
        {
            try
            {
                if (xui_effectlist.SelectedItem != null)
                {
                    var file1 = _dialogController.FindChild<IDialog_FileSelection>(_target.DialogID).SelectedFiles.FirstOrDefault();
                    if (file1 == null)
                        MessageBox.Show("No file selected");
                    else
                    {
                        try
                        {
                            var action = new Func<string, AugmentationEntry, Bitmap>((file, augmentation) =>
                            {
                                using (var m = new OpenCvSharp.Mat(file1))
                                {
                                    m.AddAlphaChannel();
                                    //temporary
                                    IAugmentationProcessor proc = (IAugmentationProcessor)Activator.CreateInstance(augmentation.Augmentation.Processor, (augmentation.Augmentation));
                                    return CreateImageV2(proc.ProcessImage(m), new OpenCvSharp.Mat(file1)).ToBitmap();
                                }
                            });
                            if (xui_effectlist.CheckedItems.Count == 1)
                                _dialogController.CreateChild<XD_Preview>(new XD_Preview(_dialogController, DialogID, action, file1, ((AugmentationEntry)xui_effectlist.SelectedItem), new string[] { ((AugmentationEntry)xui_effectlist.CheckedItems[0]).Augmentation.Name }));
                            else if (xui_effectlist.CheckedItems.Count > 1)
                                MessageBox.Show("Select a single Augmentation");
                            else
                                MessageBox.Show("Select an Augmentation");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("Checkboxes not implemented... Use selection");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static Mat CreateImageV2(AugmentationProcessorResult epr, Mat input)
        {


            if (!epr.SupportsLayering)
            {
                return epr.Layers[0].Layer;
            }
            else
            {
                //Ensure input image has alpha
                input.AddAlphaChannel();
                foreach (var layer in epr.Layers)
                {
                    input.MergeSubMat(layer.Layer, 0, 0);
                }
                return input;
            }
        }
        private Bitmap CreateImage(AugmentationProcessorResult epr, OpenCvSharp.Mat input)
        {
            if (!epr.SupportsLayering)
                return epr.Layers[0].Layer.ToBitmap();
            else
            {
                var mbmp = input.ToBitmap();
                using (var g = Graphics.FromImage(mbmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    foreach (var layer in epr.Layers)
                        using (var xbmp = layer.Layer.ToBitmap())
                        {
                            g.DrawImage(xbmp, new Rectangle(0, 0, input.Width, input.Height));
                        }

                }
                return mbmp;
            }

        }

        private void xui_runall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
        }
    }
}
