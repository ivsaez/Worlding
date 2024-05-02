using Saver;

namespace Worlding
{
    public sealed class Specials: ICloneable, ISavable
    {
        private Dictionary<string, IWorldSpecial> specials;

        public IEnumerable<IWorldSpecial> All => specials.Values;

        public Specials()
        {
            specials = new Dictionary<string, IWorldSpecial>();
        }

        public void Register(string key, IWorldSpecial special)
        {
            specials[key] = special;
        }

        public T Get<T>(string key)
            where T : class, IWorldSpecial
        {
            if (!specials.ContainsKey(key))
                throw new ArgumentException($"No special found for key {key}.");

            var result = specials[key] as T;

            if (result is null)
                throw new InvalidCastException($"Given type doesn't match the special type.");

            return result;
        }

        public object Clone()
        {
            var clone = new Specials();

            foreach (var (key, special) in specials) 
            {
                clone.specials[key] = (special.Clone() as IWorldSpecial)!;
            }

            return clone;
        }

        public Save ToSave() =>
            new Save(GetType().Name)
                .WithSavablesDictionary(nameof(specials), specials);

        public void Load(Save save)
        {
            specials = save.GetSavablesDictionary<IWorldSpecial>(nameof(specials)); 
        }
    }
}
