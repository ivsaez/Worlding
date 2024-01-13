using Agents;
using Climatics;
using Identification;
using Items;
using Logic;
using Mapping;
using StateMachine;

namespace Worlding
{
    public abstract class World : IWorld
    {
        public StateMachine<string, string> State { get; private set; }

        public Time Time { get; private set; }

        public Map Map { get; private set; }

        public ITruthTable Knowledge { get; private set; }

        public Repository<IItem> Items { get; private set; }

        public Repository<IAgent> Agents { get; private set; }

        public World()
        {
            State = StateMachine<string, string>.Create().Build();
            Time = new Time();
            Map = new Map();
            Knowledge = new TruthTable();
            Items = new Repository<IItem>();
            Agents = new Repository<IAgent>();

            initialize();
        }

        private void initialize()
        {
            initializeState();
            initializeTime();
            initializeKnowledge();
            initializeItems();
            initializeAgents();
            initializeMap();
        }

        protected abstract void initializeState();
        protected abstract void initializeTime();
        protected abstract void initializeMap();
        protected abstract void initializeKnowledge();
        protected abstract void initializeItems();
        protected abstract void initializeAgents();

    }
}
