using Agents;
using Items;
using Mapping;
using Saver;

namespace Worlding
{
    public interface IWorldItem: IItem, ISavable, ICloneable { }

    public interface IWorldAgent: IAgent, ISavable, ICloneable { }

    public interface IWorldMapped: IMapped, ISavable, ICloneable { }
}
