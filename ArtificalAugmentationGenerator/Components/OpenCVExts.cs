using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components
{
    internal static class OpenCVExts
    {
        internal static void MergeSubMat(this Mat mat, Mat submat, int r, int c)
        {
            var idxA = mat.GetGenericIndexer<Vec4b>();
            var idxB = submat.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < submat.Rows; i++)
                for (int j = 0; j < submat.Cols; j++)
                {
                    if (r + i >= mat.Rows || c + j >= mat.Cols || r + i < 0 || c + j < 0)
                        continue;
                    idxA[r + i, c + j] = AddVec4(idxA[r + i, c + j], idxB[i, j]);
                }
        }
        internal static Vec4b AddVec4(Vec4b baseColour, Vec4b overlayColour)
        {

            {
                double bA = baseColour.Item3 / 255f;
                double bR = baseColour.Item2 / 255f;
                double bG = baseColour.Item1 / 255f;
                double bB = baseColour.Item0 / 255f;

                double oA = overlayColour.Item3 / 255f;
                double oR = overlayColour.Item2 / 255f;
                double oG = overlayColour.Item1 / 255f;
                double oB = overlayColour.Item0 / 255f;

                double boA = (1 - oA) * bA + oA;
                double boR = ((1 - oA) * bA * bR + oA * oR) / boA;
                double boG = ((1 - oA) * bA * bG + oA * oG) / boA;
                double boB = ((1 - oA) * bA * bB + oA * oB) / boA;

                baseColour.Item3 = (byte)(Math.Round(255 * boA));
                baseColour.Item2 = (byte)(Math.Round(255 * boR));
                baseColour.Item1 = (byte)(Math.Round(255 * boG));
                baseColour.Item0 = (byte)(Math.Round(255 * boB));
                return baseColour;
            }
        }
        internal static void AddAlphaChannel(this Mat mat, int opacity = 255)
        {
            int chx = mat.Channels();
            if (opacity > 255 || opacity < 0)
                throw new ArgumentOutOfRangeException("opactiy", "Opacity value must be between 0 and 255");
            else if (chx < 3 || chx > 4)
                throw new ArgumentOutOfRangeException("mat", "Input matrix has either too many or not enough channels");
            if (chx == 3)
                Cv2.Merge(new Mat[] { mat, Mat.Ones(mat.Size(), MatType.CV_8UC1).Multiply(opacity) }, mat);
            //Do nothing if already has four channels
        }

        internal static void Clear(this Mat mat, System.Drawing.Color colour)
        {
            if (mat.Channels() < 3)
                throw new Exception("Too few channels!");
            else if (mat.Channels() > 4)
                throw new Exception("Too many channels!");
            else if (mat.Channels() == 3)
            {
                var idxA = mat.GetGenericIndexer<Vec3b>();
                for (int i = 0; i < mat.Rows; i++)
                    for (int j = 0; j < mat.Cols; j++)
                    {
                        var clr = idxA[i, j];
                        clr.Item0 = colour.B;
                        clr.Item1 = colour.G;
                        clr.Item2 = colour.R;
                        idxA[i, j] = clr;
                    }
            }
            else
            {
                var idxA = mat.GetGenericIndexer<Vec4b>();
                for (int i = 0; i < mat.Rows; i++)
                    for (int j = 0; j < mat.Cols; j++)
                    {
                        var clr = idxA[i, j];
                        clr.Item0 = colour.B;
                        clr.Item1 = colour.G;
                        clr.Item2 = colour.R;
                        clr.Item3 = 255;
                        idxA[i, j] = clr;
                    }
            }
        }

    }
}
