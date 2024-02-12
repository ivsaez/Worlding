using Agents;
using Climatics;
using Identification;
using Instanciation;
using Items;
using Logic;
using Mapping;
using Saver;
using StateMachine;

namespace Worlding
{
    public interface IWorld<A, I, M>
        where A : IAgent, ITimed, ISavable, ICloneable
        where I : IItem, ITimed, ISavable, ICloneable
        where M : IMapped, ITimed, ISavable, ICloneable
    {
        Machine State { get; }

        Time Time { get; }

        Map<M> Map { get; }

        ITruthTable Knowledge { get; }

        Repository<I> Items { get; }
        
        Repository<A> Agents { get; }

        IEnumerable<ITimed> Timeds { get; }

        IdGenerator Generator { get; }

        Existents<A, I, M> Existents { get; }
    }
}