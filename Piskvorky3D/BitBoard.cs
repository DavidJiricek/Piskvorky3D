using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piskvorky3D
{
    static class BitBoard
    {
        public static ulong GetLeastSignificantBit(ulong x)
        {
            return (x & (0-x));
        }

        public static string DebugPrintUlong(ulong x)
        {
            string result = "";

            for (int y = 3; y >= 0; y--)
            {
                for (int z = 0; z < 4; z++)
                {
                    result += printRow(x, z, y);
                    result += "    ";
                }
                result += "\n";
            }

            return result;
        }

        private static int isBitOccupied(ulong x, int position)
        {
            if ((((ulong)1 << position) & x) == 0)
                return 0;
            else
                return 1;
        }

        private static string printRow(ulong position, int z, int y)
        {
            int firstBitPosition = z * 16 + y * 4;
            string row = "";
            for (int i = 0; i < 4; i++)
            {
                row += isBitOccupied(position, firstBitPosition + i) + " ";
            }
            return row;
        }
    }
}
