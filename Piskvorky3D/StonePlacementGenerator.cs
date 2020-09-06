using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Piskvorky3D
{
    class StonePlacementGenerator : IGenerator<Position, ulong>
    {
        private static ulong fullFirstFloor;

        static StonePlacementGenerator()
        {
            fullFirstFloor = 0;
            for (int i = 0; i < 16; i++)
            {
                fullFirstFloor += ((ulong)1 << i);
            }
        }

        public List<ulong> GetActions(Position state)
        {
            var result = new List<ulong>();

            ulong allStones = state.White | state.Black;
            ulong stoneUnder = (allStones << 16) | fullFirstFloor;

            ulong allActions = (~allStones & stoneUnder);

            while (allActions != 0)
            {
                ulong action = BitBoard.GetLeastSignificantBit(allActions);
                allActions &= (~action);

                result.Add(action);
            }

            return result;
        }

    }
}
