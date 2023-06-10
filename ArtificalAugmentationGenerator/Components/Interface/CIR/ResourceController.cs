using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.CIR
{
    internal class ResourceController
    {
        private List<ICIRResource> _resources = new List<ICIRResource>();
        readonly DialogController _dialogController;

        //Events
        public event ResourceUpdateEventHandler ResourceUpdate;

        //Delegates
        public delegate void ResourceUpdateEventHandler(object sender, SGUID resourceID);

        //public
        public DialogController DialogController => _dialogController;

        public ResourceController(Form mdiContainer)
        {
            if (!mdiContainer.IsMdiContainer)
                throw new ArgumentException("Provided form must be MdiContainer!", "mdiContainer");
            _dialogController = new DialogController(mdiContainer);
        }

        #region Public Methods
   

        #endregion

        #region Private Methods
        private void InvokeMethod<T>(T eventField, params object[] arguments) where T : Delegate
        {
            if (eventField is null)
                return;
            foreach (var target in eventField.GetInvocationList())
                target?.DynamicInvoke(arguments);
        }
        #endregion



    }
}
