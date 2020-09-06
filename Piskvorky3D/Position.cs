using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Piskvorky3D
{
    /// <summary>
    /// Stones of each player are represented by unsigned 64bit integer (White, Black).
    /// 
    /// Index of bits in White (or Black) corresponds to positions on the board.
    /// First 16 bits are first floor of the board.
    /// Bits 17 - 32 are second floor and so on.
    /// 
    /// Bits 1 - 4 are first row of first floor. Bits 5 - 8 are second row of first floor. 
    /// </summary>
    class Position : IPosition, ICloneable
    {
        public ulong White;
        public ulong Black;
        public int SideToPlay; //1 (maximizing) or 0 (minimizing)

        public Position(ulong white, ulong black, int sideToPlay)
        {
            White = white;
            Black = black;
            SideToPlay = sideToPlay;
        }

        object ICloneable.Clone()
        {
            return new Position(White, Black, SideToPlay);
        }
    }
}
