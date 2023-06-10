using AAG_Dirt.Types.UI;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AAG_Dirt.Types
{

    [Editor(typeof(DCPEditor), typeof(UITypeEditor))]
    [RefreshProperties(RefreshProperties.All)]
    public class DirtyColourPalette
    {
        string name = "";
        bool IsSystem = false;
        public Color Colour0 { get; set; } = Color.Black;
        public Color Colour1 { get; set; } = Color.Black;
        public Color Colour2 { get; set; } = Color.Black;
        public Color Colour3 { get; set; } = Color.Black;
        public Color Colour4 { get; set; } = Color.Black;
        public Color Colour5 { get; set; } = Color.Black;
        public Color Colour6 { get; set; } = Color.Black;
        public Color Colour7 { get; set; } = Color.Black;

        public string Name => name;

        public DirtyColourPalette(string name)
        {
            this.name = name;

        }

        public override string ToString()
        {
            return name;
        }

        internal void InheritColours(DirtyColourPalette s)
        {
            Colour0 = s.Colour0;
            Colour1 = s.Colour1;
            Colour2 = s.Colour2;
            Colour3 = s.Colour3;
            Colour4 = s.Colour4;
            Colour5 = s.Colour5;
            Colour6 = s.Colour6;
            Colour7 = s.Colour7;
        }

        internal Color[] Collect()
        {
            return new Color[]{
                Colour0,Colour1,Colour2,Colour3,Colour4,Colour5,Colour6,Colour7
            };
        }

        internal void SetNameCustom()
        {
            name = "Custom";
        }

        public static string ConvertToString(DirtyColourPalette value)
        {
            if (KnownDirtyColourPalettes.SystemPalettes.FirstOrDefault(x => x.Name.Equals(((DirtyColourPalette)value).Name)) != null)
                return ((DirtyColourPalette)value).Name;
            return $"{value.Colour0.ToArgb().ToString()},{value.Colour1.ToArgb().ToString()},{value.Colour2.ToArgb().ToString()},{value.Colour3.ToArgb().ToString()},{value.Colour4.ToArgb().ToString()},{value.Colour5.ToArgb().ToString()},{value.Colour6.ToArgb().ToString()},{value.Colour7.ToArgb().ToString()}";
        }
        public static DirtyColourPalette ConvertFromString(string value)
        {
            if (KnownDirtyColourPalettes.SystemPalettes.Select(x => x.Name).Contains(value, StringComparer.InvariantCultureIgnoreCase))
                return KnownDirtyColourPalettes.SystemPalettes.FirstOrDefault(x => x.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            if (!Regex.IsMatch(value, "^[0-9-]*,[0-9-]*,[0-9-]*,[0-9-]*,[0-9-]*,[0-9-]*,[0-9-]*,[0-9-]*$"))
                //throw new Exception("Invalid Input");
                return null;
            string[] nummers = value.Split(',');
            return new DirtyColourPalette("Unnamed Palette")
            {
                Colour0 = Color.FromArgb(int.Parse(nummers[0])),
                Colour1 = Color.FromArgb(int.Parse(nummers[1])),
                Colour2 = Color.FromArgb(int.Parse(nummers[2])),
                Colour3 = Color.FromArgb(int.Parse(nummers[3])),
                Colour4 = Color.FromArgb(int.Parse(nummers[4])),
                Colour5 = Color.FromArgb(int.Parse(nummers[5])),
                Colour6 = Color.FromArgb(int.Parse(nummers[6])),
                Colour7 = Color.FromArgb(int.Parse(nummers[7]))

            };
        }

        internal Scalar[] CollectScalar()
        {
            return new Scalar[]{
                ToScalar(Colour0),
                ToScalar(Colour1),
                ToScalar(Colour2),
                ToScalar(Colour3),
                ToScalar(Colour4),
                ToScalar(Colour5),
                ToScalar(Colour6),
                ToScalar(Colour7)
            };
        }
        private Scalar ToScalar(Color colour)
        {
            return new Scalar(colour.B, colour.G, colour.R, colour.A );
        }
    }

    internal static class KnownDirtyColourPalettes
    {
        public static DirtyColourPalette[] SystemPalettes => new DirtyColourPalette[] { Palette1, Palette2, Palette3, Fog, Black };
        public static DirtyColourPalette Palette1 = new DirtyColourPalette("Palette1")
        {
            Colour0 = Color.FromArgb(92, 71, 40),
            Colour1 = Color.FromArgb(78, 61, 33),
            Colour2 = Color.FromArgb(66, 51, 28),
            Colour3 = Color.FromArgb(52, 41, 23),
            Colour4 = Color.FromArgb(39, 29, 17),
            Colour5 = Color.FromArgb(27, 20, 10),
            Colour6 = Color.FromArgb(13, 10, 5),
            Colour7 = Color.FromArgb(0, 0, 0)
        };
        public static DirtyColourPalette Palette2 = new DirtyColourPalette("Palette2")
        {
            Colour0 = Color.FromArgb(211, 176, 143),
            Colour1 = Color.FromArgb(184, 151, 124),
            Colour2 = Color.FromArgb(133, 86, 58),
            Colour3 = Color.FromArgb(96, 49, 19),
            Colour4 = Color.FromArgb(56, 30, 15),
            Colour5 = Color.FromArgb(107, 75, 44),
            Colour6 = Color.FromArgb(73, 47, 22),
            Colour7 = Color.FromArgb(127, 107, 89)
        };
        public static DirtyColourPalette Palette3 = new DirtyColourPalette("Palette3")
        {
            Colour0 = Color.FromArgb(226, 203, 169),
            Colour1 = Color.FromArgb(172, 145, 116),
            Colour2 = Color.FromArgb(175, 137, 74),
            Colour3 = Color.FromArgb(137, 110, 81),
            Colour4 = Color.FromArgb(141, 98, 47),
            Colour5 = Color.FromArgb(70, 49, 28),
            Colour6 = Color.FromArgb(63, 34, 16),
            Colour7 = Color.FromArgb(34, 21, 13)
        };
        public static DirtyColourPalette Fog = new DirtyColourPalette("Fog")
        {
            Colour0 = Color.White,
            Colour1 = Color.WhiteSmoke,
            Colour2 = Color.AntiqueWhite,
            Colour3 = Color.FloralWhite,
            Colour4 = Color.GhostWhite,
            Colour5 = Color.NavajoWhite,
            Colour6 = Color.Silver,
            Colour7 = Color.LightSlateGray
        };
        public static DirtyColourPalette Black = new DirtyColourPalette("Black")
        {
            Colour0 = Color.Black,
            Colour1 = Color.Black,
            Colour2 = Color.Black,
            Colour3 = Color.Black,
            Colour4 = Color.Black,
            Colour5 = Color.Black,
            Colour6 = Color.Black,
            Colour7 = Color.Black
        };
    }
}
