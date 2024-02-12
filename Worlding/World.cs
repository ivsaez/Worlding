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
    public class World<A, I, M> : IWorld<A, I, M>, ISavable, ICloneable
        where A : IAgent, ITimed, ISavable, ICloneable
        where I : IItem, ITimed, ISavable, ICloneable
        where M : IMapped, ITimed, ISavable, ICloneable
    {
        public Machine State { get; private set; }

        public Time Time { get; private set; }

        public ITruthTable Knowledge { get; private set; }

        public Map<M> Map { get; private set; }

        public Repository<I> Items { get; private set; }

        public Repository<A> Agents { get; private set; }

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
            Map = new Map<M>();
            Knowledge = new TruthTable();
            Items = new Repository<I>();
            Agents = new Repository<A>();
            Generator = new IdGenerator();
        }

        public Existents<A, I, M> Existents =>
            new Existents<A, I, M>(Agents, Items, Map);

        public object Clone()
        {
            var clone = new World<A, I, M>((Machine)State.Clone());
            clone.Time = (Time)Time.Clone();
            clone.Map = (Map<M>)Map.Clone();
            clone.Knowledge = (ITruthTable)Knowledge.Clone();
            clone.Items = (Repository<I>)Items.Clone();
            clone.Agents = (Repository<A>)Agents.Clone();
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
            Map = save.GetSavable<Map<M>>(nameof(Time));
            Knowledge = save.GetSavable<ITruthTable>(nameof(Time));
            Items = save.GetSavable<Repository<I>>(nameof(Time));
            Agents = save.GetSavable<Repository<A>>(nameof(Time));
            Generator = save.GetSavable<IdGenerator>(nameof(Generator));
        }
    }
}
