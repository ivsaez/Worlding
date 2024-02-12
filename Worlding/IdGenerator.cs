using Saver;

namespace Worlding
{
    public class IdGenerator: ISavable, ICloneable
    {
        private uint agentsSeed;
        private uint itemsSeed;
        private uint mappedsSeed;

        public IdGenerator()
        {
            agentsSeed = 0;
            itemsSeed = 0;
            mappedsSeed = 0;
        }

        public string NextAgentId
        {
            get
            {
                agentsSeed++;
                return $"agent-{agentsSeed}";
            }
        }

        public string NextItemId
        {
            get
            {
                itemsSeed++;
                return $"item-{itemsSeed}";
            }
        }

        public string NextMappedId
        {
            get
            {
                mappedsSeed++;
                return $"mapped-{mappedsSeed}";
            }
        }

        public object Clone()
        {
            var clone = new IdGenerator();
            clone.agentsSeed = agentsSeed;
            clone.itemsSeed = itemsSeed;
            clone.mappedsSeed = mappedsSeed;

            return clone;
        }

        public void Load(Save save)
        {
            agentsSeed = save.GetUInt(nameof(agentsSeed));
            itemsSeed = save.GetUInt(nameof(itemsSeed));
            mappedsSeed = save.GetUInt(nameof(mappedsSeed));
        }

        public Save ToSave() =>
            new Save(GetType().Name)
                .With(nameof(agentsSeed), agentsSeed)
                .With(nameof(itemsSeed), itemsSeed)
                .With(nameof(mappedsSeed), mappedsSeed);
    }
}
