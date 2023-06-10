using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Interface
{
    public class SGUID
    {
        static Random rand = new Random();
        private static SGUID _empty = new SGUID('0', '0', '0', '0', '0');
        public const string ALLOWEDCHARACTERS = "ABCDEF0123456789";

        private char[] data;

        public static SGUID Empty => _empty;

        public SGUID()
        {
            data = _empty.data;
        }
        public SGUID(string sguid)
        {
            if (sguid.Length != 5)
                throw new Exception("Invalid SGUID length. Expected 5");
            if (Check(sguid.ToUpper().ToCharArray()))
                data = sguid.ToUpper().ToCharArray();
            else
                throw new Exception("Invalid HEX characters");

        }
        public SGUID(char[] sguid)
        {
            if (sguid.Length != 5)
                throw new Exception("Invalid SGUID length. Expected 5");
            if (Check(sguid))
                data = sguid;
            else
                throw new Exception("Invalid HEX characters");

        }
        public SGUID(char b1, char b2, char b3, char b4, char b5)
        {
            if (Check(b1, b2, b3, b4, b5))
            {
                data = new char[] { b1, b2, b3, b4, b5 };
            }
            else
                throw new Exception("Invalid HEX characters");
        }

        private bool Check(params char[] bx)
        {

            foreach (var b in bx)
            {
                if (!ALLOWEDCHARACTERS.Contains(b))
                    // throw new Exception($"{b} is an invalid character for SGUID");
                    return false;
            }
            return true;
        }

        public static SGUID NewSGUID()
        {
            char[] data = new char[5];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = ALLOWEDCHARACTERS[rand.Next(0, ALLOWEDCHARACTERS.Length)];
            }
            return new SGUID(data);
        }

        public override bool Equals(object obj)
        {
            if (obj is SGUID)
            {
                return ((SGUID)obj).data[0].Equals(data[0]) && ((SGUID)obj).data[1].Equals(data[1]) && ((SGUID)obj).data[2].Equals(data[2]) && ((SGUID)obj).data[3].Equals(data[3]) && ((SGUID)obj).data[4].Equals(data[4]);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return new string(data).GetHashCode();
        }

        public override string ToString()
        {
            return new string(data);
        }
    }
}
