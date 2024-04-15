using Items;
using Outputer;
using Saver;

namespace Worlding
{
    public abstract class WorldItem: Item, IWorldItem
    {
        private TurnPassed? turnPassed;

        protected WorldItem(string id, uint space, uint weight)
            : base(id, space, weight)
        {
        }

        public object Clone()
        {
            var cloneItem = (WorldItem)clone();

            cloneItem.Id = Id;
            cloneItem.Space = Space;
            cloneItem.Weight = Weight;

            return cloneItem;
        }

        protected abstract object clone();

        public Save ToSave()
        {
            var saveItem = save();

            saveItem.With(nameof(Id), Id);
            saveItem.With(nameof(Space), Space);
            saveItem.With(nameof(Weight), Weight);

            return saveItem;
        }

        protected abstract Save save();

        public void Load(Save save)
        {
            Id = save.GetString(nameof(Id));
            Space = save.GetUInt(nameof(Space));
            Weight = save.GetUInt(nameof(Weight));
        }

        protected abstract void load(Save save);

        public WorldItem WithTurnPassed(TurnPassed turnPassed)
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
