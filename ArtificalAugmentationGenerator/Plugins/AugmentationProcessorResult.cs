using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins
{
    public class AugmentationProcessorResult : IDisposable
    {
        List<EPRLayer> _layers = new List<EPRLayer>();
        bool _supportsLayering = false;

        public IReadOnlyList<EPRLayer> Layers => _layers.AsReadOnly();
        public bool IsSingleLayered => _layers.Count == 1;

        public bool SupportsLayering => _supportsLayering;

        /// <summary>
        /// Return result of Effect Processor Processing method
        /// </summary>
        /// <param name="supportsLayering">If true, program will allow effect to be merged with other effects. If false, first layer must original image or opaque</param>
        public AugmentationProcessorResult(bool supportsLayering)
        {
            _supportsLayering = supportsLayering;
        }

        public static AugmentationProcessorResult CreateResult(Mat image, bool supportsLayering)
        {
            var epr = new AugmentationProcessorResult(supportsLayering);
            epr.AddLayer(image);
            return epr;
        }
        public static AugmentationProcessorResult CreateResult(Mat[] images, bool supportsLayering)
        {
            var epr = new AugmentationProcessorResult(supportsLayering);
            foreach (var mat in images)
                epr.AddLayer(mat);
            return epr;
        }

        public void AddLayer(Mat layer, bool visible = true)
        {
            _layers.Add(new EPRLayer() { Layer = layer, Visible = visible });
        }

        public void Dispose()
        {
            foreach (var layer in Layers)
                layer.Layer.Dispose();
        }

        public struct EPRLayer
        {
            public Mat Layer { get; set; }
            public bool Visible { get; set; }

        }
    }
}
