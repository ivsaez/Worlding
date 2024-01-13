using Agents;
using Climatics;
using Identification;
using Items;
using Logic;
using Mapping;
using StateMachine;

namespace Worlding
{
    public interface IWorld
    {
        StateMachine<string, string> State { get; }

        Time Time { get; }

        Map Map { get; }

        ITruthTable Knowledge { get; }

        Repository<IItem> Items { get; }
        
        Repository<IAgent> Agents { get; }
    }
}