using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Dirt
{
    internal static class Common
    {
        /// <summary>
        /// Performs image composting for OCVProcessor
        /// </summary>
        /// <param name="background">Background Image</param>
        /// <param name="foreground">Foreground image</param>
        public static void MergeWithAlpha(this Mat background, Mat foreground)
        {
            var idxA = background.GetGenericIndexer<Vec4b>();
            var idxB = foreground.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < background.Rows; i++)
                for (int j = 0; j < foreground.Cols; j++)
                {
                    idxA[i,j] = AddVec4(idxA[i,j], idxB[i, j]);
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
    }
}
