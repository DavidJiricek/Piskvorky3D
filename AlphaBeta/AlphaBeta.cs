using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace AlphaBeta
{
    public class AlphaBeta<S, A> where S : ICloneable
    {
        private IGame<S, A> game;

        public AlphaBeta(IGame<S, A> game)
        {
            this.game = game;
        }

        public double Alphabeta_recursive(ref List<A> moveSequence, int depth, S pos, double alpha, double beta)
        {
            //nodesVisited++;
            double value;
            if (depth == 0 || game.IsFinished(pos))
            {
                return game.Eval(pos);
            }

            List<A> moves = game.Generator.GetActions(pos);

            if (game.PlayerOnTurn(pos) == 1)
            {
                value = -double.MaxValue;
                foreach (var move in moves)
                {
                    var newPos = (S)pos.Clone();
                    game.Apply(ref newPos, move);

                    List<A> newMoveSequence = new List<A>();
                    double newValue = Alphabeta_recursive(ref newMoveSequence, depth - 1, newPos, alpha, beta);
                    if (newValue > value)
                    {
                        moveSequence = new List<A>(newMoveSequence);
                        moveSequence.Add(move);
                        value = newValue;
                        alpha = Math.Max(alpha, value);
                    }
                    if (alpha >= beta) break;
                }
            }
            else
            {
                value = Double.MaxValue;
                foreach (var move in moves)
                {
                    S newPos = (S)pos.Clone();
                    game.Apply(ref newPos, move);

                    var newMoveSequence = new List<A>();
                    double newValue = Alphabeta_recursive(ref newMoveSequence, depth - 1, newPos, alpha, beta);
                    if (newValue < value)
                    {
                        moveSequence = new List<A>(newMoveSequence);
                        moveSequence.Add(move);
                        value = newValue;
                        beta = Math.Min(beta, value);
                    }
                    if (alpha >= beta) break;
                }
            }
            return value;
        }
    }
}
