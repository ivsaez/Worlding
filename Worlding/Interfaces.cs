using Agents;
using Items;
using Mapping;
using Saver;

namespace Worlding
{
    public interface IWorldAgent : IAgent, ITimed, ISavable, ICloneable
    {
    }

    public interface IWorldItem : IItem, ITimed, ISavable, ICloneable
    {
    }

    public interface IWorldMapped : IMapped, ITimed, ISavable, ICloneable
    {
    }
}
