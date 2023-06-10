using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Controls
{
    public partial class LabelledTextbox : UserControl
    {
        public int LabelWidth { get => ui_label.Width; set => ui_label.Width = value; }
        public Label Label => ui_label;
        public TextBox Textbox => ui_text;
        public LabelledTextbox()
        {
            InitializeComponent();
        }
    }
}
