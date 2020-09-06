using AlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Piskvorky3D
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Piskvorky3D();
            var alphabeta = new AlphaBeta<Position, ulong>(game);

            var moves = new List<ulong>();

            while (true)
            {
                var eval = alphabeta.Alphabeta_recursive(ref moves, 8, game.InitialState, double.MinValue, double.MaxValue);
                Console.WriteLine("Eval: " + eval);
                moves.Reverse();
                var state = game.InitialState; //todo clone

                game.Apply(ref state, moves.First());
                Console.WriteLine(BitBoard.DebugPrintUlong(state.White) + "\n");
                Console.WriteLine(BitBoard.DebugPrintUlong(state.Black) + "\n");

                int opponentMove = int.Parse(Console.ReadLine()); // todo zkontrolovat 1-16
                var possibleMoves = game.Generator.GetActions(state);

       
                ulong ONE = (ulong)1;
                ulong column = (ONE) + (ONE << 16) + (ONE << 32) + (ONE << 48);
                column = (column << (opponentMove - 1));

                foreach (var move in possibleMoves)
                {
                    if ((column & move)!=0)
                    {
                        game.Apply(ref state, move);
                        break;
                    }
                }

                Console.WriteLine(BitBoard.DebugPrintUlong(state.White) + "\n");
                Console.WriteLine(BitBoard.DebugPrintUlong(state.Black) + "\n");
            }
            

            //var generator = new StonePlacementGenerator();
            //
            //var pos = new Position(0, 0, 1);
            //pos.White |= ((ulong)1 << 5);
            //pos.White |= ((ulong)1 << 8);
            //pos.White |= ((ulong)1 << 0);
            //pos.White |= ((ulong)1 << 16);
            //
            //var a = generator.GetActions(pos);
            //
            //Console.WriteLine(BitBoard.DebugPrintUlong(pos.White) + "\n");
            //Console.WriteLine(BitBoard.DebugPrintUlong(pos.Black) + "\n");
            //
            //foreach (var item in a)
            //{
            //    Console.WriteLine(BitBoard.DebugPrintUlong(item) + "\n");
            //}
        }
    }
}
