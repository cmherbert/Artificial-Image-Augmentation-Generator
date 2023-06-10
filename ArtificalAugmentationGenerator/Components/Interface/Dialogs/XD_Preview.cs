using ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces;
using ArtificalAugmentationGenerator.Plugins;
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
using static ArtificalAugmentationGenerator.Components.Interface.Dialogs.XD_AugmentationSelection;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    internal partial class XD_Preview : Form, IDialog_Common
    {
        private SGUID _id = SGUID.NewSGUID();
        private readonly SGUID _parent;
        private readonly DialogController _dialogController;
        private readonly Func<string, AugmentationEntry, Bitmap> _generator;
        private readonly string _source;
        private readonly AugmentationEntry _effect;

        public SGUID DialogID => _id;
        public XD_Preview(DialogController controller, SGUID parent, Func<string, AugmentationEntry, Bitmap> generateImage, string source, AugmentationEntry effect, string[] effects = null)
        {
            InitializeComponent();
            _dialogController = controller;
            _generator = generateImage;
            _parent = parent;
            _source = source;
            _effect = effect;
            xui_image.Image = _generator(_source,_effect);
            xui_imagesource.Text = source ?? "Generated Image";
            if (effects != null)
                xui_effectname.Text = string.Join(",", effects);
            else
                xui_effectname.Text = "No effects used";
            xui_sourceid.Textbox.Text = _id.ToString();
            xui_sourceid.Textbox.ReadOnly = true;
            xui_sourceid.Label.Text = "ID:";
            xui_targetid.Label.Text = "Target:";
            xui_targetid.Textbox.ReadOnly = true;
            xui_targetid.Textbox.Text = $"{_parent.ToString()}";

        }

        private void copyToClip_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(xui_image.Image);
        }

        private void xui_refresh_Click(object sender, EventArgs e)
        {
            xui_image.Image = _generator(_source, _effect);
        }

        private void saveImage_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = Path.Combine(saveFileDialog1.InitialDirectory, Path.GetFileName(_source));
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                xui_image.Image.Save(saveFileDialog1.FileName, Path.GetExtension(saveFileDialog1.FileName).ToUpper() == ".PNG" ? System.Drawing.Imaging.ImageFormat.Png : System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
