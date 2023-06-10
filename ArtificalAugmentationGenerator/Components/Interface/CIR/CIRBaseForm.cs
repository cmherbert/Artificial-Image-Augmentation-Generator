using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.CIR
{
    internal class CIRBaseForm : Form, ICIRResource
    {
        //Designer
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1 = new ToolStripStatusLabel();

        //Private
        private SGUID _resourceID = SGUID.NewSGUID();
        private SGUID _parentID = SGUID.Empty;
        readonly ResourceController _controller;
        private StatusStrip statusStrip1;
        private bool _autoApplyTBStyle = true;
        private List<SGUID> _trackedResources = new List<SGUID>();
        private ToolStrip _masterToolStrip = null;

        //Public
        public SGUID ResourceID => _resourceID;
        public bool AutoApplyToolStripStyling => _autoApplyTBStyle;
        public string StatusText { get => toolStripStatusLabel1.Text; set => toolStripStatusLabel1.Text = value; }

        //Protected
        protected ResourceController Controller => _controller;

        private CIRBaseForm()
        {
            InitializeComponent();
        }

        internal CIRBaseForm(ResourceController controller, SGUID parent)
        {
            _controller = controller;
            _parentID = parent;
            Init();
            InitializeComponent();
        }

        private void Init()
        {
            Controller.DialogController.MdiEvent += DialogController_MdiEvent;
            Controller.ResourceUpdate += Controller_ResourceUpdate;
        }

        private void Controller_ResourceUpdate(object sender, SGUID resourceID)
        {
            if (_trackedResources.Contains(resourceID))
                OnResourceUpdated(resourceID);
        }

        private void DialogController_MdiEvent(object sender, SGUID resourceID, DialogControllerMDIEventAction action)
        {
            if (resourceID.Equals(_parentID) && action == DialogControllerMDIEventAction.Closed)
                OnParentClosed();
        }

        protected virtual void OnParentClosed()
        {
            Close();
        }

        protected virtual void OnResourceUpdated(SGUID resourceID)
        {

        }

        protected void InvokeMethod<T>(T eventField, params object[] arguments) where T : Delegate
        {
            if (eventField is null)
                return;
            foreach (var target in eventField.GetInvocationList())
                target?.DynamicInvoke(arguments);
        }

        #region ToolstripStyling
        protected override void OnShown(EventArgs e)
        {
            if (AutoApplyToolStripStyling)
                foreach (var control in Controls)
                {
                    if (control is ToolStrip && !(control is StatusStrip))
                    {
                    
                        if (((ToolStrip)(control)).Dock == DockStyle.Top && ((ToolStrip)(control)).Parent is Form)
                            _masterToolStrip = ((ToolStrip)(control));
                    }
                }
            base.OnShown(e);
        }
        protected override void WndProc(ref Message m)
        {
            var parentForm = FindForm();
            switch (m.Msg)
            {
                case 0x0006:    //WM_ACTIVATE
                case 0x0086:    //WM_NCACTIVATE
                    ToggleBGClr(m.WParam.ToInt32() == 0 ? false : true);
                    break;
                case 0x0222:    //WM_MDIACTIVATE
                    if (parentForm != null && parentForm.IsMdiChild)
                    {
                        ToggleBGClr(parentForm.Handle.Equals(m.LParam) ? false : true);
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        protected override void OnNotifyMessage(Message m)
        {
            var parentForm = FindForm();
            switch (m.Msg)
            {
                case 0x0006:    //WM_ACTIVATE
                case 0x0086:    //WM_NCACTIVATE
                    ToggleBGClr(m.WParam.ToInt32() == 0 ? false : true);
                    break;
                case 0x0222:    //WM_MDIACTIVATE
                    if (parentForm != null && parentForm.IsMdiChild)
                    {
                        ToggleBGClr(parentForm.Handle.Equals(m.LParam) ? false : true);
                    }
                    break;
            }
            base.OnNotifyMessage(m);
        }



        /// <summary>
        /// Method changes background colour of toolstrip to match MDI child titlebar
        /// </summary>
        /// <param name="active"></param>
        private void ToggleBGClr(bool active)
        {
            if (_masterToolStrip != null)
            {
                if (active)
                    _masterToolStrip.BackColor = SystemColors.GradientActiveCaption;
                else
                    _masterToolStrip.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

       
        #endregion

        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 303);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(407, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // CIRBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(407, 325);
            this.Controls.Add(this.statusStrip1);
            this.Name = "CIRBaseForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
