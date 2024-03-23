using Items;
using Outputer;
using Saver;

namespace Worlding
{
    public abstract class WorldItem: Item, IWorldItem
    {
        protected WorldItem(string id)
            : base(id)
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

        public Output OnTurnPassed(IWorld world, uint turns) =>
            onTurnPassed(world, turns);

        protected abstract Output onTurnPassed(IWorld world, uint turns);
    }
}
