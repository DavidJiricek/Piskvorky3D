using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Piskvorky3D
{
    class Piskvorky3D : IGame<Position, ulong>
    {
        public List<ulong> winningMasks = generateWinningMasks();
        public IGenerator<Position, ulong> Generator { get; } = new StonePlacementGenerator();
        public Position InitialState { get; } = new Position(0, 0, 1);


        public void Apply(ref Position state, ulong stonePlacement)
        {
            if (state.SideToPlay == 0) //black, minimizing
            {
                state.Black |= stonePlacement;
                state.SideToPlay = 1;
            }
            else
            {
                state.White |= stonePlacement;
                state.SideToPlay = 0;
            }
        }

        public Position Clone(Position state)
        {
            throw new NotImplementedException();
        }

        public bool IsFinished(Position state)
        {
            foreach (var mask in winningMasks)
            {
                if ((state.Black & mask) == mask)
                    return true;
                if ((state.White & mask) == mask)
                    return true;
            }
            //all the positions are occupied
            if ((state.Black | state.White) == ulong.MaxValue)
                return true;
            return false;
        }

        public double WhoIsWinner(Position state)
        {
            if (!IsFinished(state))
                throw new Exception("Game is not finished!");

            foreach (var mask in winningMasks)
            {
                if ((state.Black & mask) == mask)
                    return 0;
                if ((state.White & mask) == mask)
                    return 1;
            }
            return 0.5;
        }

        public double Eval(Position state)
        {
            if (IsFinished(state))
                return WhoIsWinner(state);
            else return 0.5;
        }

        private static List<ulong> generateWinningMasks()
        {
            ulong ONE = (ulong)1;
            var masks = new List<ulong>();
            //x rows
            ulong row = (ONE) + (ONE << 1) + (ONE << 2) + (ONE << 3);
            for (int i = 0; i < 16; i++)
            {
                masks.Add(row << (4 * i));
            }
            //y rows
            row = ONE + (ONE << 4) + (ONE << 8) + (ONE << 12);
            //each floor
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    masks.Add(row << (j + i*16));
                }
            }
            //z columns
            row = (ONE) + (ONE << 16) + (ONE << 32) + (ONE << 48);
            for (int i = 0; i < 16; i++)
            {
                masks.Add(row << i);
            }
            //x-z diagonals
            row = (ONE) + (ONE << 17) + (ONE << 34) + (ONE << 51);
            for (int i = 0; i < 4; i++)
            {
                masks.Add(row << (4*i));
            }
            //z-x diagonals
            row = (ONE << 3) + (ONE << 18) + (ONE << 33) + (ONE << 48);
            for (int i = 0; i < 4; i++)
            {
                masks.Add(row << (4 * i));
            }
            //y-z diagonals
            row = (ONE) + (ONE << 20) + (ONE << 40) + (ONE << 60);
            for (int i = 0; i < 4; i++)
            {
                masks.Add(row << i);
            }
            //z-y diagonals
            row = (ONE << 12) + (ONE << 24) + (ONE << 36) + (ONE << 48);
            for (int i = 0; i < 4; i++)
            {
                masks.Add(row << i);
            }
            //x-y diagonals
            row = (ONE) + (ONE << 5) + (ONE << 10) + (ONE << 15);
            for (int i = 0; i < 4; i++)
            {
                masks.Add(row << (i*16));
            }
            //y-x diagonals
            row = (ONE << 3) + (ONE << 6) + (ONE << 9) + (ONE << 12);
            for (int i = 0; i < 4; i++)
            {
                masks.Add(row << (i * 16));
            }
            //main diagonals
            row = (ONE) + (ONE << 21) + (ONE << 42) + (ONE << 63);
            masks.Add(row);
            row = (ONE << 3) + (ONE << 22) + (ONE << 41) + (ONE << 60);
            masks.Add(row);
            row = (ONE << 12) + (ONE << 25) + (ONE << 38) + (ONE << 51);
            masks.Add(row);
            row = (ONE << 15) + (ONE << 26) + (ONE << 37) + (ONE << 48);
            masks.Add(row);

            return masks;
        }

        public int PlayerOnTurn(Position state)
        {
            return state.SideToPlay; //0=minimizing (black), 1=maximizing (white)
        }
    }
}
