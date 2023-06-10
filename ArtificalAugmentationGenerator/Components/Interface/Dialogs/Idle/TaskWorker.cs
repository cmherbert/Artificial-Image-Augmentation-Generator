using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs.Idle
{
    public partial class TaskWorker : Form
    {
        readonly Func<object> _task;
        object result = null;
        public TaskWorker(Func<object> task)
        {
            InitializeComponent();
            _task = task;

        }

        protected override void OnShown(EventArgs e)
        {
            Task.Run(() =>
            {
                result = _task();
                CloseDialog();

            });

        }

        private void CloseDialog()
        {
            this.Invoke(new Action(() =>
            {
                this.Close();
            }));
        }

        public static object ShowWorkerAsync(IWin32Window parent, Func<object> task)
        {
            using (var taskworker = new TaskWorker(task))
            {

                taskworker.ShowDialog();
                return taskworker.result;
            }
        }
    }
}
