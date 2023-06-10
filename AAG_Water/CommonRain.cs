using AAG_Water.Types;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;

namespace AAG_Water
{
    /// <summary>
    /// Provides shared code for RainAugmentation
    /// </summary>
    internal static class CommonRain
    {
        static readonly Mat Template_Alpha;
        static readonly Mat Template_Normal;

        /// <summary>
        /// Colour Matrix Kernel
        /// </summary>
        static readonly Mat kCM = Mat.FromArray(new float[,] { { 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 19, -9 } }) / 13;

        /// <summary>
        /// Sharpening Kernel
        /// </summary>
        static readonly Mat kSRP = Mat.FromArray(new float[,] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } });

        static CommonRain()
        {
            Template_Alpha = Properties.Resources.RETemplateC1.ToMat();
            Template_Normal = Properties.Resources.RETemplateC2.ToMat();

        }
        internal static List<RainDrop> CreateDrops(Random random, int syX, int syY, int szX, int szY, int tolerance, int drops, Size minSz, Size maxSz)
        {
            #region Create Drops
            List<RainDrop> rainDrops = new List<RainDrop>();
            for (int i = 0; i < drops; i++)
            {
                var sx = random.Next(syX, szX);
                var sy = random.Next(syY, szY);
                var sw = random.Next(minSz.Width, maxSz.Width);
                var sh = random.Next(minSz.Height, maxSz.Height);
                rainDrops.Add(new RainDrop(sx, sy, sw, sh));
            }
            return rainDrops.Merge(tolerance).Crop(szX, szY);
            #endregion

        }

        /// <summary>
        /// Draws each raindrop onto individual Matrix and recolours using Template_Normal
        /// </summary>
        /// <param name="rainDrops">Merged Raindrops</param>
        /// <param name="blobifyOpts">Custom options for blobification process - see class</param>
        /// <returns>Array of normalised raindrops on seperate matricies</returns>
        internal static Mat[] RenderNormalDrops(List<RainDrop> rainDrops, BlobifyOptions blobifyOpts = null)
        {
            Mat[] drops = new Mat[rainDrops.Count];
            Mat image_sdw = Template_Alpha;
            Mat image_clr = Template_Normal;
            if (image_sdw.Channels() == 3)
            {
                //Add Alpha
                var chx = image_sdw.Split();
                Cv2.Merge(new Mat[] { chx[0], chx[1], chx[2], Mat.Ones(image_sdw.Rows, image_sdw.Cols, MatType.CV_8UC1).Multiply(255).ToMat() }, image_sdw);
            }
            if (image_clr.Channels() == 3)
            {
                //Add Alpha
                var chx = image_clr.Split();
                Cv2.Merge(new Mat[] { chx[0], chx[1], chx[2], Mat.Ones(image_clr.Rows, image_clr.Cols, MatType.CV_8UC1).Multiply(255).ToMat() }, image_clr);
            }

            #region Render Drops
            //Draw Drops
            for (int i = 0; i < rainDrops.Count; i++)
            {
                Mat templateDrop = Mat.Zeros(rainDrops[i].Bounds.Size.Height, rainDrops[i].Bounds.Size.Width, MatType.CV_8UC4);
                using (Mat templateNormal = image_clr.Clone().Resize(new Size(rainDrops[i].Bounds.Size.Width, rainDrops[i].Bounds.Size.Height), interpolation: InterpolationFlags.Cubic))
                {
                    for (int j = 0; j < rainDrops[i].Drops.Count; j++)
                    {
                        //Check Raindrop Contained within Box for now

                        using (var jdrop = image_sdw.Clone().Resize(new Size(rainDrops[i].Drops[j].Width, rainDrops[i].Drops[j].Height), interpolation: InterpolationFlags.Cubic))
                        {
                            var submat_x = rainDrops[i].Drops[j].X - rainDrops[i].Bounds.X;
                            var submat_y = rainDrops[i].Drops[j].Y - rainDrops[i].Bounds.Y;
                            templateDrop.MergeSubMat(jdrop, submat_y, submat_x);
                        }

                    }
                    //Recolour
                    templateDrop.RecolourMat(templateNormal);
                    templateDrop.Blobify(blobifyOpts);
                    drops[i] = templateDrop;
                }
            }
            return drops;
            #endregion
        }

        /// <summary>
        /// Merges a smaller matrix onto a base matrix at specified offset
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="mOverlay">Overlay matrix</param>
        /// <param name="r">Row offset</param>
        /// <param name="c">Column offset</param>
        internal static void MergeSubMat(this Mat mBase, Mat mOverlay, int r, int c)
        {
            var idxA = mBase.GetGenericIndexer<Vec4b>();
            var idxB = mOverlay.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < mOverlay.Rows; i++)
                for (int j = 0; j < mOverlay.Cols; j++)
                {
                    if (r + i >= mBase.Rows || c + j >= mBase.Cols || r + i < 0 || c + j < 0)
                        continue;
                    idxA[r + i, c + j] = AddVec4(idxA[r + i, c + j], idxB[i, j]);
                }
        }
        /// <summary>
        /// Recolours base matrix using non-transparent pixels of mask
        /// </summary>
        /// <param name="mBase">base matrix</param>
        /// <param name="mMask">mask matrix</param>
        internal static void RecolourMat(this Mat mBase, Mat mMask)
        {
            var bindex = mBase.GetGenericIndexer<Vec4b>();
            var mindex = mMask.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < mBase.Rows; i++)
                for (int j = 0; j < mBase.Cols; j++)
                {
                    if (j > mBase.Cols || i > mBase.Rows)
                        continue;
                    Vec4b baseclr = bindex[i, j];
                    Vec4b maskclr = mindex[i, j];
                    maskclr.Item3 = 255;
                    if (baseclr.Item3 > 0)
                        bindex[i, j] = maskclr;
                    else
                        bindex[i, j] = new Vec4b(128, 128, 128, 0);
                }
        }
        /// <summary>
        /// Recolours base matrix using normal matrix.
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="mNormal">Normal matrix (Must be 256 x 256)</param>
        /// <param name="options">Custom options for recolouring - see RecolourOptions</param>
        internal static void RecolourNormalMat(this Mat mBase, Mat mNormal, RecolourOptions options = null)
        {
            var bindex = mBase.GetGenericIndexer<Vec4b>();
            var mindex = mNormal.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < mBase.Rows; i++)
                for (int j = 0; j < mBase.Cols; j++)
                {
                    if (i > mBase.Rows || j > mBase.Cols)
                        continue;
                    Vec4b baseclr = bindex[i, j];
                    if (baseclr.Item3 > 0 && (options?.PixelFiltering?.Invoke(baseclr) ?? true))
                    {
                        int y = GetRel(255 - baseclr.Item1, 0, 255, 0, mNormal.Height - 1);
                        int x = GetRel(255 - baseclr.Item2, 0, 255, 0, mNormal.Width - 1);
                        var a = mindex[y, x];
                        var b = bindex[i, j];
                        a.Item3 = b.Item3;
                        bindex[i, j] = options?.PostProcessingAdjustment?.Invoke(a) ?? a;
                    }
                }

        }

        /// <summary>
        /// rescales value to fit onto a new range
        /// </summary>
        /// <param name="value">Value within current range</param>
        /// <param name="valMin">Minimum value of current range</param>
        /// <param name="valMax">Maximum value of current range</param>
        /// <param name="min">Minimum value of desired range</param>
        /// <param name="max">Maximum value of desired range</param>
        /// <returns>Value within desired range</returns>
        private static int GetRel(int value, int valMin, int valMax, int min, int max)
        {
            double x = ((double)value - valMin) / (valMax - valMin);
            int y = (int)Math.Min(max, Math.Max(min, (min + (max - min) * x)));
            return y;
        }

        /// <summary>
        /// Performs Alpha blending on two pixel colours for image composting
        /// </summary>
        /// <param name="vBase">Base colour</param>
        /// <param name="vOverlay">Overlay colour</param>
        /// <returns>Blended colour</returns>
        internal static Vec4b AddVec4(Vec4b vBase, Vec4b vOverlay)
        {

            {
                double bA = vBase.Item3 / 255f;
                double bR = vBase.Item2 / 255f;
                double bG = vBase.Item1 / 255f;
                double bB = vBase.Item0 / 255f;

                double oA = vOverlay.Item3 / 255f;
                double oR = vOverlay.Item2 / 255f;
                double oG = vOverlay.Item1 / 255f;
                double oB = vOverlay.Item0 / 255f;

                double boA = (1 - oA) * bA + oA;
                double boR = ((1 - oA) * bA * bR + oA * oR) / boA;
                double boG = ((1 - oA) * bA * bG + oA * oG) / boA;
                double boB = ((1 - oA) * bA * bB + oA * oB) / boA;

                vBase.Item3 = (byte)(Math.Round(255 * boA));
                vBase.Item2 = (byte)(Math.Round(255 * boR));
                vBase.Item1 = (byte)(Math.Round(255 * boG));
                vBase.Item0 = (byte)(Math.Round(255 * boB));
                return vBase;
            }
        }

        /// <summary>
        /// Creates a solid white matrix
        /// </summary>
        /// <param name="r">number of rows</param>
        /// <param name="c">number of columns</param>
        /// <returns></returns>
        internal static Mat CreateWhiteMat(int r, int c)
        {
            Mat rx = new Mat(r, c, MatType.CV_8UC4);
            Cv2.Merge(new Mat[] { Mat.Ones(r, c, MatType.CV_8UC1).Multiply(255), Mat.Ones(r, c, MatType.CV_8UC1).Multiply(255), Mat.Ones(r, c, MatType.CV_8UC1).Multiply(255), Mat.Ones(r, c, MatType.CV_8UC1).Multiply(255) }, rx);
            return rx;
        }

        /// <summary>
        /// Performs blobification on a matrix
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="options">BlobificationOptions - see BlobifyOptions</param>
        internal static void Blobify(this Mat mBase, BlobifyOptions options = null)
        {
            Cv2.GaussianBlur(mBase, mBase, new OpenCvSharp.Size(51, 51), 3);
            Cv2.Filter2D(mBase, mBase, -1, options?.KernelColourMatrix ?? kCM, anchor: new OpenCvSharp.Point(-1, -1), borderType: BorderTypes.Default);
            if (options?.Sharpen ?? false)
            {
                Cv2.Filter2D(mBase, mBase, -1, options?.KernelSharpenMatrix ?? kSRP);
            }
        }

        /// <summary>
        /// Prepares a matrix to be used for refraction
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="intensity">Refraction strength</param>
        /// <param name="options">Refraction Options - see RefractOptions</param>
        /// <returns>Refracted matrix</returns>
        internal static Mat Refract(this Mat mBase, int intensity, RefractOptions options = null)
        {
            options?.PreRefractionAction?.Invoke(mBase);
            var bz = intensity;
            bz = bz % 2 == 1 ? bz : bz + 1;
            //if (brightness != 0)
            //    mat += new Scalar(brightness, brightness, brightness, 0);
            Cv2.GaussianBlur(mBase, mBase, new Size(bz, bz), 1);
            Cv2.MedianBlur(mBase, mBase, Math.Min(bz, 15));
            Cv2.Blur(mBase, mBase, new Size(bz, bz));
            Cv2.Flip(mBase, mBase, FlipMode.XY);
            return mBase;
        }

        /// <summary>
        /// Creates refraction bounds through inflating input rectangle with size, then clamps to edges
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="rect">Target bounds</param>
        /// <param name="size">Inflation amount</param>
        /// <returns>Safe refraction bounds</returns>
        internal static Rect CreateSafeRefractionRect(Mat mBase, System.Drawing.Rectangle rect, int size)
        {
            var bg = new Rect(Clamp(rect.X - size / 2, 0, mBase.Width - 2), Clamp(rect.Y - size / 2, 0, mBase.Height - 2), rect.Width, rect.Height);
            bg.Width = Clamp(rect.Width + size, 1, mBase.Width - bg.X - 1);
            bg.Height = Clamp(rect.Height + size, 1, mBase.Height - bg.Y - 1);
            return bg;
        }

        /// <summary>
        /// Clamps value between bounds
        /// </summary>
        /// <param name="val">value</param>
        /// <param name="min">minimum range</param>
        /// <param name="max">maximum range</param>
        /// <returns>clamped value</returns>
        private static int Clamp(int val, int min, int max)
        {
            if (min > max)
                return min;
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }

        /// <summary>
        /// Recolours each normalised raindrop with refracted image behind and merges into a single matrix
        /// </summary>
        /// <param name="rainDrops">Raindrop bounds, provides sub-matrix positioning</param>
        /// <param name="mNDrops">Normalised Raindrops</param>
        /// <param name="mBase">Base matrix (used for sizing)</param>
        /// <param name="size">Refraction size</param>
        /// <param name="intensity">Refraction Intensity</param>
        /// <param name="refract_options">Refraction options - see RefractOptions</param>
        /// <param name="recolour_options">Recolour options - see RecolourOptions</param>
        /// <returns>Single matrix with refracted raindrops on transparent background </returns>
        internal static Mat RenderRefraction(List<RainDrop> rainDrops, ref Mat[] mNDrops, Mat mBase, int size, int intensity, RefractOptions refract_options = null, RecolourOptions recolour_options = null)
        {
            if (mBase.Channels() == 3)
            {
                var chx = mBase.Split();
                Cv2.Merge(new Mat[] { chx[0], chx[1], chx[2], Mat.Ones(mBase.Rows, mBase.Cols, MatType.CV_8UC1).Multiply(255).ToMat() }, mBase);
            }
            Mat rimage = Mat.Zeros(mBase.Rows, mBase.Cols, MatType.CV_8UC4);
            for (int i = 0; i < rainDrops.Count; i++)
            {
                if (mNDrops[i] is null || rainDrops[i].Bounds.Right > mBase.Width || rainDrops[i].Bounds.Bottom > mBase.Height)
                    continue;
                //Grab image behind
                Rect bg = CreateSafeRefractionRect(mBase, rainDrops[i].Bounds, size);

                using (Mat subclone = mBase.Clone(bg).Refract(intensity, refract_options).Resize(new Size(rainDrops[i].Bounds.Width, rainDrops[i].Bounds.Height), interpolation: InterpolationFlags.Linear))
                {
                    mNDrops[i].RecolourNormalMat(subclone, recolour_options);
                    //Uncommenting this line renders Raindrop bounds
                    //rnd[i].Rectangle(new Rect(0, 0, rnd[i].Width, rnd[i].Height), new Scalar(255, 0, 0, 255), 3);

                    rimage.MergeSubMat(mNDrops[i], rainDrops[i].Bounds.Y, rainDrops[i].Bounds.X);

                    //Uncommenting this line renders Refraction bounds
                    //rimage.Rectangle(bg, new Scalar(0, 0, 255, 255), 3);
                }

            }
            return rimage;
        }

        /// <summary>
        /// Strengthens edges produced by canny edge detection
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="mCanny">Canny matrix</param>
        /// <exception cref="Exception"></exception>
        internal static void InflateCanny(this Mat mBase, Mat mCanny)
        {
            if (mBase.Channels() > 1 || mCanny.Channels() != 4)
                throw new Exception("Invalid Channel Numbers. Input expects 1, output 4");
            if (mBase.Size() != mCanny.Size())
                throw new Exception("Invalid Size");
            var idxA = mBase.GetGenericIndexer<byte>();
            var idxB = mCanny.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < mBase.Rows; i++)
                for (int j = 0; j < mBase.Cols; j++)
                {
                    idxB[i, j] = idxA[i, j] == 0 ? new Vec4b(0, 0, 0, 0) : new Vec4b(200, 200, 200, 200);
                }


        }

        /// <summary>
        /// Increases opacity of each pixel in base matrix by specified percentage
        /// </summary>
        /// <param name="mBase">Base Matrix</param>
        /// <param name="percentage">Percentage adjustment</param>
        /// <exception cref="Exception"></exception>
        internal static void AdjustOpacity(this Mat mBase, double percentage)
        {
            if (mBase.Channels() != 4)
                throw new Exception("Invalid Channel Numbers. Input expects 4");

            var idxA = mBase.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < mBase.Rows; i++)
                for (int j = 0; j < mBase.Cols; j++)
                {
                    var vec = idxA[i, j];
                    vec.Item3 = (byte)Math.Max(0, Math.Min(255, ((int)(((double)idxA[i, j].Item3) * percentage))));
                    idxA[i, j] = vec;
                }
        }

        /// <summary>
        /// Recolours all pixels with an opactiy greater than threshold to the provided colour
        /// </summary>
        /// <param name="mBase">Base matrix</param>
        /// <param name="vColour">Recolour colour</param>
        /// <param name="bThresh">Opacitiy threshold, default: 60</param>
        public static void Edge(this Mat mBase, Vec4b vColour, byte bThresh= 60)
        {
            var bindex = mBase.GetGenericIndexer<Vec4b>();
            for (int i = 0; i < mBase.Rows; i++)
                for (int j = 0; j < mBase.Cols; j++)
                {
                    Vec4b baseclr = bindex[i, j];
                    if (baseclr.Item3 > bThresh)
                    {
                        bindex[i, j] = vColour;
                    }
                }
        }

        public class BlobifyOptions
        {
            /// <summary>
            /// Enables / Disables Sharpening
            /// </summary>
            public bool Sharpen { get; set; } = false;

            /// <summary>
            /// Specifies matrix used for perorming colour filter operation
            /// </summary>
            public Mat KernelColourMatrix { get; set; } = null;

            /// <summary>
            /// Specifies matrix used for performing sharpening operation
            /// </summary>
            public Mat KernelSharpenMatrix { get; set; } = null;
        }
        public class RefractOptions
        {
            /// <summary>
            /// Preprocessing action
            /// </summary>
            public Action<Mat> PreRefractionAction { get; set; } = null;
        }
        public class RecolourOptions
        {
            /// <summary>
            /// Specifies criteria used for pixel filtering
            /// </summary>
            public Predicate<Vec4b> PixelFiltering { get; set; } = null;

            /// <summary>
            /// PostProcessing action
            /// </summary>
            public Func<Vec4b, Vec4b> PostProcessingAdjustment { get; set; } = null;
        }
    }
}

