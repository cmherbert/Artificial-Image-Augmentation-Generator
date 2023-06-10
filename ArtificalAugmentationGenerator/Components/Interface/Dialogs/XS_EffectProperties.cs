using ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces;
using ArtificalAugmentationGenerator.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; // _dialogController.MdiEvent += _dialogController_MdiEvent;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    internal partial class XS_EffectProperties : Form, IDialog_Common
    {
        private readonly DialogController _dialogController;
        private readonly SGUID _parentTarget;
        private readonly Augmentation _effect;
        private SGUID _id = SGUID.NewSGUID();

        public SGUID DialogID => _id;
        public XS_EffectProperties(DialogController controller, SGUID parentInfo, Augmentation effect)
        {
            _dialogController = controller;
            _parentTarget = parentInfo;
            _effect = effect;
           //// // _dialogController.MdiEvent += _dialogController_MdiEvent;
            InitializeComponent();
            xui_sourceid.Textbox.Text = _id.ToString();
            xui_sourceid.Textbox.ReadOnly = true;
            xui_sourceid.Label.Text = "ID:";
            xui_targetid.Label.Text = "Target:";
            xui_targetid.Textbox.ReadOnly = true;
            xui_targetid.Textbox.Text = $"{parentInfo.ToString()}::{effect.Name}";
            xui_effectname.Text = effect.Name;
            xui_props.SelectedObject = _effect;

        }

        private void _dialogController_MdiEvent(object sender, EventArgs e)
        {
            //Close if parent closes
            var form = _dialogController.FindChild<IDialog_Common>(_parentTarget);
            if (form == null)
                this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            //_dialogController.MdiEvent -= _dialogController_MdiEvent;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ResetEffectProperties();
        }

        private void ResetEffectProperties()
        {
            try
            {
                var nObj = Activator.CreateInstance(_effect.GetType());
                foreach (var prop in _effect.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (prop.CanWrite && prop.CanRead)
                    {
                        prop.SetValue(_effect, prop.GetValue(nObj));
                    }
                }
            }
            catch
            {
                MessageBox.Show("An error occured while trying to reset properties");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
