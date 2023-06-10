using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace AAG_Dirt.Types.UI
{
    /// <summary>
    /// Provides PropertyGrid control for DirtyColourPalette types a dropdown of known colours
    /// Known colours are defined in DirtyColourPalette class
    /// </summary>
    internal class DCPEditor : UITypeEditor
    {
        public DCPEditor()
        {

        }
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            //return true;
            return base.GetPaintValueSupported(context);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService es = null;
            es = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (es != null)
            {
                var DCP = (DirtyColourPalette)value;
                var dcpe = new ListView();
                var lvi = new ListViewItem[KnownDirtyColourPalettes.SystemPalettes.Length];
                for (int i = 0; i < lvi.Length; i++)
                {
                    lvi[i] = new ListViewItem(KnownDirtyColourPalettes.SystemPalettes[i].Name + "                              ") { Selected = (value is null && i == 0) || ((value as DirtyColourPalette).Name.Equals(KnownDirtyColourPalettes.SystemPalettes[i].Name)) };
                }
                dcpe.Items.AddRange(lvi);
                dcpe.DrawItem += Dcpe_DrawItem;
                dcpe.View = View.List;

                dcpe.OwnerDraw = true;
                dcpe.MultiSelect = false;
                dcpe.ItemActivate += (s, e) => { try { es.CloseDropDown(); } catch { } };
                es.DropDownControl(dcpe);

                return KnownDirtyColourPalettes.SystemPalettes.FirstOrDefault(x => x.Name == dcpe.SelectedItems[0].Text.Trim());

            }
            else
                return base.EditValue(context, provider, value);
        }

        private void Dcpe_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var item = KnownDirtyColourPalettes.SystemPalettes.FirstOrDefault(x => x.Name == e.Item.Text.Trim());

            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                float mw = e.Bounds.Width / 2 / 9;
                float xx = 0;
                float yy = e.Bounds.Height / 2 - mw / 2;
                foreach (Color clr in (item.Collect()))
                {
                    e.Graphics.FillRectangle(new SolidBrush(clr), e.Bounds.X + xx, e.Bounds.Y + yy, mw, mw);
                    e.Graphics.DrawRectangle(Pens.Black, e.Bounds.X + xx, e.Bounds.Y + yy, mw - 1, mw - 1);
                    xx += mw / 9 + mw;
                }
                xx += mw / 9;
                e.Graphics.DrawString(item.Name, SystemFonts.DefaultFont, Brushes.Black, new RectangleF(e.Bounds.X + xx, e.Bounds.Y, e.Bounds.Width - xx, e.Bounds.Height), new StringFormat() { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap });
            }

        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override void PaintValue(PaintValueEventArgs e)
        {
            base.PaintValue(e);
        }
    }
}
