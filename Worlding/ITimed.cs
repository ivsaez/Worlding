using Agents;
using Items;
using Mapping;
using Outputer;
using Saver;

namespace Worlding
{
    public interface ITimed
    {
        Output OnTurnPassed<A, I, M>(IWorld<A, I, M> world, int turns)
            where A : IAgent, ITimed, ISavable, ICloneable
            where I : IItem, ITimed, ISavable, ICloneable
            where M : IMapped, ITimed, ISavable, ICloneable;
    }
}
