using Items;
using Mapping;
using Outputer;
using Saver;

namespace Worlding
{
    public abstract class WorldMapped : Mapped, IWorldMapped
    {
        protected WorldMapped(string id) 
            : base(id)
        {
        }

        public object Clone()
        {
            var cloneMapped = (WorldMapped)clone();

            cloneMapped.Id = Id;
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
            saveMapped.WithSavable(nameof(Agents), Agents);
            saveMapped.WithSavable(nameof(Items), Items);
            saveMapped.WithSavable(nameof(Exits), Exits);

            return saveMapped;
        }

        protected abstract Save save();

        public void Load(Save save)
        {
            Id = save.GetString(nameof(Id));
            Agents = save.GetSavable<Mapping.Agents>(nameof(Agents));
            Items = save.GetSavable<Inventory>(nameof(Items));
            Exits = save.GetSavable<Exits>(nameof(Exits));
        }

        protected abstract void load(Save save);

        public Output OnTurnPassed(IWorld world, uint turns) =>
            onTurnPassed(world, turns);

        protected abstract Output onTurnPassed(IWorld world, uint turns);
    }
}
