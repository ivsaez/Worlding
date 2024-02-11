using Climatics;
using Identification;
using Instanciation;
using Logic;
using Mapping;
using Saver;
using StateMachine;

namespace Worlding
{
    public class World : IWorld, ISavable, ICloneable
    {
        public Machine State { get; private set; }

        public Time Time { get; private set; }

        public ITruthTable Knowledge { get; private set; }

        public Map<IWorldMapped> Map { get; private set; }

        public Repository<IWorldItem> Items { get; private set; }

        public Repository<IWorldAgent> Agents { get; private set; }

        public IEnumerable<ITimed> Timeds => new List<ITimed>()
            .Concat(Items.All)
            .Concat(Agents.All)
            .Concat(Map.Mappeds);

        public World(
            Machine state,
            Time time,
            Map<IWorldMapped> map,
            ITruthTable knowledge,
            Repository<IWorldItem> items,
            Repository<IWorldAgent> agents)
        {
            State = state;
            Time = time;
            Map = map;
            Knowledge = knowledge;
            Items = items;
            Agents = agents;
        }

        public Existents<IWorldAgent, IWorldItem, IWorldMapped> Existents =>
            new Existents<IWorldAgent, IWorldItem, IWorldMapped>(Agents, Items, Map);

        public object Clone() =>
            new World(
                (Machine)State.Clone(),
                (Time)Time.Clone(),
                (Map<IWorldMapped>)Map.Clone(),
                (ITruthTable)Knowledge.Clone(),
                (Repository<IWorldItem>)Items.Clone(),
                (Repository<IWorldAgent>)Agents.Clone());

        public Save ToSave() =>
            new Save(GetType().Name)
                .WithSavable(nameof(State), State)
                .WithSavable(nameof(Time), Time)
                .WithSavable(nameof(Map), Map)
                .WithSavable(nameof(Knowledge), Knowledge)
                .WithSavable(nameof(Items), Items)
                .WithSavable(nameof(Agents), Agents);

        public void Load(Save save)
        {
            State = save.GetSavable<Machine>(nameof(State));
            Time = save.GetSavable<Time>(nameof(Time));
            Map = save.GetSavable<Map<IWorldMapped>>(nameof(Time));
            Knowledge = save.GetSavable<ITruthTable>(nameof(Time));
            Items = save.GetSavable<Repository<IWorldItem>>(nameof(Time));
            Agents = save.GetSavable<Repository<IWorldAgent>>(nameof(Time));
        }
    }
}
