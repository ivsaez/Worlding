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

        public IdGenerator Generator { get; private set; }

        public IEnumerable<ITimed> Timeds =>
            new List<ITimed>()
                .Concat(Items.All.Cast<ITimed>())
                .Concat(Agents.All.Cast<ITimed>())
                .Concat(Map.Mappeds.Cast<ITimed>());

        public World(Machine state)
        {
            State = state;
            Time = new Time();
            Map = new Map<IWorldMapped>();
            Knowledge = new TruthTable();
            Items = new Repository<IWorldItem>();
            Agents = new Repository<IWorldAgent>();
            Generator = new IdGenerator();
        }

        public Turneds PassTurn(uint turns)
        {
            Time.IncreaseMinutes(turns);

            var turneds = new Turneds();

            foreach (var timed in Timeds) 
            {
                var output = timed.OnTurnPassed(this, turns);
                turneds.Add(timed.Id, output);
            }

            return turneds;
        }

        public Existents<IWorldAgent, IWorldItem, IWorldMapped> Existents =>
            new Existents<IWorldAgent, IWorldItem, IWorldMapped>(Agents, Items, Map);

        public object Clone()
        {
            var clone = new World((Machine)State.Clone());
            clone.Time = (Time)Time.Clone();
            clone.Map = (Map<IWorldMapped>)Map.Clone();
            clone.Knowledge = (ITruthTable)Knowledge.Clone();
            clone.Items = (Repository<IWorldItem>)Items.Clone();
            clone.Agents = (Repository<IWorldAgent>)Agents.Clone();
            clone.Generator = (IdGenerator)Generator.Clone();

            return clone;   
        }

        public Save ToSave() =>
            new Save(GetType().Name)
                .WithSavable(nameof(State), State)
                .WithSavable(nameof(Time), Time)
                .WithSavable(nameof(Map), Map)
                .WithSavable(nameof(Knowledge), Knowledge)
                .WithSavable(nameof(Items), Items)
                .WithSavable(nameof(Agents), Agents)
                .WithSavable(nameof(Generator), Generator);

        public void Load(Save save)
        {
            State = save.GetSavable<Machine>(nameof(State));
            Time = save.GetSavable<Time>(nameof(Time));
            Map = save.GetSavable<Map<IWorldMapped>>(nameof(Time));
            Knowledge = save.GetSavable<ITruthTable>(nameof(Time));
            Items = save.GetSavable<Repository<IWorldItem>>(nameof(Time));
            Agents = save.GetSavable<Repository<IWorldAgent>>(nameof(Time));
            Generator = save.GetSavable<IdGenerator>(nameof(Generator));
        }
    }
}
