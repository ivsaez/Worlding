using Climatics;
using Identification;
using Instanciation;
using Logic;
using Mapping;
using StateMachine;

namespace Worlding
{
    public interface IWorld
    {
        Machine State { get; }

        Time Time { get; }

        Map<IWorldMapped> Map { get; }

        ITruthTable Knowledge { get; }

        Repository<IWorldItem> Items { get; }
        
        Repository<IWorldAgent> Agents { get; }

        IEnumerable<ITimed> Timeds { get; }

        IdGenerator Generator { get; }

        Existents<IWorldAgent, IWorldItem, IWorldMapped> Existents { get; }

        Turneds PassTurn(uint turns);
    }
}