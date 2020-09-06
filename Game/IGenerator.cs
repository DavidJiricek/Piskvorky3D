using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IGenerator<S, A>
    {
        List<A> GetActions(S state);  // actions to try in this state
    }
}
