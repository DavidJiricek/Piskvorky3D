using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    // S = state type, A = action type
    public interface IGame<S, A>
    {
        S InitialState { get; }
        S Clone(S state);
        int PlayerOnTurn(S state);         // which player moves next: 1 (maximizing) or 2 (minimizing)
        void Apply(ref S state, A action);  // apply action to state, possibly with stochastic result
        bool IsFinished(S state);     // true if game has finished
        double Eval(S state);     // 1.0 = player 1 wins, 0.5 = draw, 0.0 = player 2 wins
        IGenerator<S, A> Generator { get; }
    }
}
