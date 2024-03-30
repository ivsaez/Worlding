using Agents;
using Outputer;
using Saver;

namespace Worlding
{
    public abstract class WorldAgent : Agent, IWorldAgent
    {
        protected WorldAgent(
            string id,
            string name,
            string surname,
            Importance importance)
            : base(id, name, surname, importance)
        {
        }

        public object Clone()
        {
            var cloneAgent = (WorldAgent)clone();

            cloneAgent.Status = (Status)Status.Clone();
            cloneAgent.Position = (Position)Position.Clone();

            if (Actioner == Actioner.Human)
                cloneAgent.BecomeHuman();
            else
                cloneAgent.BecomeIA();

            return cloneAgent;
        }

        protected abstract object clone();

        public Save ToSave()
        {
            var saveAgent = save();

            saveAgent.With(nameof(Id), Id);
            saveAgent.With(nameof(Name), Name);
            saveAgent.With(nameof(Surname), Surname);
            saveAgent.With(nameof(Importance), (int)Importance);
            saveAgent.WithSavable(nameof(Status), Status);
            saveAgent.WithSavable(nameof(Position), Position);

            if (Actioner == Actioner.Human)
                saveAgent.With(nameof(Actioner), true);
            else
                saveAgent.With(nameof(Actioner), false);

            return saveAgent;
        }

        protected abstract Save save();

        public void Load(Save save)
        {
            Id = save.GetString(nameof(Id));
            Name = save.GetString(nameof(Name));
            Surname = save.GetString(nameof(Surname));
            Importance = (Importance)save.GetInt(nameof(Importance));
            Status = save.GetSavable<Status>(nameof(Status));
            Position = save.GetSavable<Position>(nameof(Position));

            var actioner = save.GetBool(nameof(Actioner));
            if (actioner)
                BecomeHuman();
            else
                BecomeIA();
        }

        protected abstract void load(Save save);

        public Output OnTurnPassed(IWorld world, uint turns) =>
            onTurnPassed(world, turns);

        protected abstract Output onTurnPassed(IWorld world, uint turns);
    }
}
