using ArtificalAugmentationGenerator.Components.Interface.CIR;
using ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface
{
    internal class DialogController
    {
        readonly Form _form;
        public event MDIEventHandler MdiEvent;
        List<SGUID> _trackedWindows = new List<SGUID>();
        SGUID _lastActiveChild = SGUID.Empty;

        public delegate void MDIEventHandler(object sender, SGUID resourceID, DialogControllerMDIEventAction action);

        public DialogController(Form form)
        {
            _form = form;
            _form.MdiChildActivate += _form_MdiChildActivate;
        }

        public List<T> FindAllChildren<T>() where T : class, IDialog_Common
        {
            List<T> dialogs = new List<T>();
            foreach (var mdichild in _form.MdiChildren)
            {
                if (mdichild.Disposing)
                    continue;
                if (mdichild is T)
                    dialogs.Add(mdichild as T);
            }
            return dialogs;
        }
        public T FindChild<T>(SGUID target) where T : class, IDialog_Common
        {
            return _form.MdiChildren.FirstOrDefault(x => x is T && (x as T).DialogID.Equals(target) && (!x.Disposing)) as T;
        }
        public SGUID CreateChild<T>(T form) where T : Form, IDialog_Common
        {
            form.MdiParent = _form;
            form.Show();
            return form.DialogID;
        }

        private void _form_MdiChildActivate(object sender, EventArgs e)
        {
            var newChild = _form.ActiveMdiChild;
            if (newChild is ICIRResource)
            {
                if (newChild.Disposing || newChild.IsDisposed)
                {

                    InvokeMethod(MdiEvent, this, _lastActiveChild, DialogControllerMDIEventAction.Closed);
                    _trackedWindows.Remove(_lastActiveChild);
                    _lastActiveChild = SGUID.Empty;
                }
                else
                {
                    if (_lastActiveChild != SGUID.Empty)
                        InvokeMethod(MdiEvent, this, _lastActiveChild, DialogControllerMDIEventAction.Deactivated);
                    if (!_trackedWindows.Contains(((ICIRResource)newChild).ResourceID))
                    {
                        _trackedWindows.Add(((ICIRResource)newChild).ResourceID);
                        InvokeMethod(MdiEvent, this, _lastActiveChild, DialogControllerMDIEventAction.Created);
                    }
                    else
                    {
                        InvokeMethod(MdiEvent, this, _lastActiveChild, DialogControllerMDIEventAction.Created);
                    }
                    _lastActiveChild = ((ICIRResource)newChild).ResourceID;
                }

              
            }
        }

        private void InvokeMethod<T>(T eventField, params object[] arguments) where T : Delegate
        {
            if (eventField is null)
                return;
            foreach (var target in eventField.GetInvocationList())
                target?.DynamicInvoke(arguments);
        }


    }
    internal enum DialogControllerMDIEventAction
    {
        Created,
        Activated,
        Deactivated,
        Closed
    }
}
