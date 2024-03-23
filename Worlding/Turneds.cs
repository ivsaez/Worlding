using Outputer;

namespace Worlding
{
    public class Turneds
    {
        private readonly Dictionary<string, Output> turneds;

        public IEnumerable<Output> All => turneds.Values;

        public Output AllCombined =>
            All.Aggregate(new Output(), (acumulator, output) => acumulator.With(output));

        public Turneds()
        {
            turneds = new Dictionary<string, Output>();
        }

        public void Add(string id, Output output)
        {
            checkValidId(id);

            if (!output.IsEmpty)
                turneds.Add(id, output);
        }

        public Output Get(string id)
        {
            checkValidId(id);

            if (!turneds.ContainsKey(id))
                return Output.Empty;

            return turneds[id];
        }

        public IEnumerable<Output> GetMany(params string[] ids) =>
            ids.Select(id => Get(id))
                .Where(output => !output.IsEmpty)
                .ToList();

        public Output GetManyCombined(params string[] ids) =>
            GetMany(ids).Aggregate(new Output(), (acumulator, output) => acumulator.With(output));

        private static void checkValidId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
        }
    }
}
