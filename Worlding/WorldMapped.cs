using Items;
using Mapping;
using Outputer;
using Saver;

namespace Worlding
{
    public abstract class WorldMapped : Mapped, IWorldMapped
    {
        private TurnPassed? turnPassed;

        protected WorldMapped(string id, Externality externality) 
            : base(id, externality)
        {
        }

        public object Clone()
        {
            var cloneMapped = (WorldMapped)clone();

            cloneMapped.Agents = (Mapping.Agents)Agents.Clone();
            cloneMapped.Items = (Inventory)Items.Clone();
            cloneMapped.Exits = (Exits)Exits.Clone();

            return cloneMapped;
        }

        protected abstract object clone();

        public Save ToSave()
        {
            var saveMapped = save();

            saveMapped.With(nameof(Id), Id);
            saveMapped.With(nameof(Externality), (int)Externality);
            saveMapped.WithSavable(nameof(Agents), Agents);
            saveMapped.WithSavable(nameof(Items), Items);
            saveMapped.WithSavable(nameof(Exits), Exits);

            return saveMapped;
        }

        protected abstract Save save();

        public void Load(Save save)
        {
            Id = save.GetString(nameof(Id));
            Externality = (Externality)save.GetInt(nameof(Externality));
            Agents = save.GetSavable<Mapping.Agents>(nameof(Agents));
            Items = save.GetSavable<Inventory>(nameof(Items));
            Exits = save.GetSavable<Exits>(nameof(Exits));
        }

        protected abstract void load(Save save);

        public WorldMapped WithTurnPassed(TurnPassed turnPassed)
        {
            if (this.turnPassed is not null)
                throw new InvalidOperationException("Turn passed already assigned.");

            this.turnPassed = turnPassed;
            return this;
        }

        public Output OnTurnPassed(IWorld world, uint turns)
        {
            if (turnPassed is not null)
                return turnPassed(world, turns);

            return Output.Empty;
        }
    }
}
