using ArtificalAugmentationGenerator.Components.CMDLine;
using ArtificalAugmentationGenerator.Components.Interface.CIR;
using ArtificalAugmentationGenerator.Components.Presets.Models;
using ArtificalAugmentationGenerator.Plugins;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs
{
    internal partial class XD_Performance : Form
    {
        internal XD_Performance()
        {
            InitializeComponent();
            InitialiseValues();
        }

        private void InitialiseValues(List<EffectEntry> entries = null)
        {
            xui_effectlist.Items.Clear();
            if (entries == null)
            {
                foreach (var effect in ContentManager.Augmentations)
                {
                    xui_effectlist.Items.Add(new EffectEntry((Augmentation)Activator.CreateInstance(effect.Item.GetType())));
                }
            }
            else
            {
                entries.ForEach(x => xui_effectlist.Items.Add(x));
            }
            Status.Text = $"{xui_effectlist.Items.Count} Effects";
        }

        private struct EffectEntry
        {
            IAugmentation _effect;
            string _nameOverride;
            public IAugmentation Effect => _effect;

            public EffectEntry(IAugmentation entry)
            {
                _effect = entry;
                _nameOverride = null;
            }
            public EffectEntry(IAugmentation entry, string nameOverride)
            {
                _effect = entry;
                _nameOverride = nameOverride;
            }
            public override string ToString()
            {
                return _nameOverride ?? _effect.Name;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            Status.Text = "Running test...";

            Stopwatch sw = new Stopwatch();
            foreach (EffectEntry effect in xui_effectlist.CheckedItems)
            {
                sw.Reset();
                var task = Task.Run(() =>
                {
                    //Create Effect Processor
                    IAugmentationProcessor proc = (IAugmentationProcessor)Activator.CreateInstance(effect.Effect.Processor, effect.Effect);
                    for (int i = 0; i < 5; i++)
                        proc.ProcessImage(Properties.Resources.Onemana244.ToMat());
                    sw.Start();
                    for (int i = 0; i < 5; i++)
                        proc.ProcessImage(Properties.Resources.Onemana244.ToMat());
                    sw.Stop();
                });
                task.Wait();
                //Update text
                var elapsed = TimeSpan.FromMilliseconds(sw.Elapsed.TotalMilliseconds / 5d);
                if (elapsed.TotalMilliseconds <= 300)
                    richTextBox1.ForeColor = Color.Red;
                else if (elapsed.TotalMilliseconds <= 150)
                    richTextBox1.ForeColor = Color.Orange;

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = elapsed.TotalMilliseconds >= 600 ? Color.Red : elapsed.TotalMilliseconds >= 300 ? Color.Orange : Color.Black;
                richTextBox1.AppendText($"Effect {effect.ToString()} completed in {FriendlyTimespan(elapsed)}\r\n");
                richTextBox1.SelectionColor = Color.Black;
            }
            richTextBox1.AppendText("Done!");
            Status.Text = $"{xui_effectlist.Items.Count} Effects";
        }


        private string FriendlyTimespan(TimeSpan ts)
        {
            if (ts.TotalHours < 1)
                return $"{ts.ToString("mm':'ss'.'fff")}";
            return ts.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //todo: replace with content manager presets
            using (var files = new OpenFileDialog())
            {
                if (files.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PresetXML pxml = PresetXML.CreateFromFile(files.FileName);
                        List<EffectEntry> entries = new List<EffectEntry>();
                        foreach (var preset in pxml.Presets)
                        {
                            var epp = new AugmentationPresetPair(ContentManager.Augmentations.First(x => x.Item.Name.Equals(preset.Augmentation, StringComparison.InvariantCultureIgnoreCase)).Item.GetType(), preset);
                            epp.ApplyLargeRandomisation(epp.Effect);
                            epp.ApplySmallRandomisation(epp.Effect);
                            entries.Add(new EffectEntry(epp.Effect, $"{epp.Effect.Name}::{epp.Preset.Name}"));
                        }
                        InitialiseValues(entries);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitialiseValues(null);
        }

        private void xui_selectnone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < xui_effectlist.Items.Count; i++)
                xui_effectlist.SetItemChecked(i, false);
        }

        private void xui_selectall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < xui_effectlist.Items.Count; i++)
                xui_effectlist.SetItemChecked(i, true);
        }

        private void xui_invertselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < xui_effectlist.Items.Count; i++)
                xui_effectlist.SetItemChecked(i, !xui_effectlist.GetItemChecked(i));
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
