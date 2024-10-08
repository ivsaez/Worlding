using Saver;

namespace Worlding
{
    public class Props : ISavable, ICloneable
    {
        private Dictionary<string, int> integers;
        private Dictionary<string, bool> booleans;
        private Dictionary<string, string> strings;

        public Props()
        {
            integers = new Dictionary<string, int>();
            booleans = new Dictionary<string, bool>();
            strings = new Dictionary<string, string>();
        }

        public Props With(string key, int value)
        {
            checkPropExistence(key, PropType.Int, false);
            checkPropExistence(key, PropType.Bool, false);
            checkPropExistence(key, PropType.String, false);

            integers[key] = value;

            return this;
        }

        public Props With(string key, bool value)
        {
            checkPropExistence(key, PropType.Int, false);
            checkPropExistence(key, PropType.Bool, false);
            checkPropExistence(key, PropType.String, false);

            booleans[key] = value;

            return this;
        }

        public Props With(string key, string value)
        {
            checkPropExistence(key, PropType.Int, false);
            checkPropExistence(key, PropType.Bool, false);
            checkPropExistence(key, PropType.String, false);

            strings[key] = value;

            return this;
        }

        public int GetInt(string key)
        {
            checkPropExistence(key, PropType.Int, true);

            return integers[key];
        }

        public bool GetBool(string key)
        {
            checkPropExistence(key, PropType.Bool, true);

            return booleans[key];
        }

        public string GetString(string key)
        {
            checkPropExistence(key, PropType.String, true);

            return strings[key];
        }

        public void SetInt(string key, int value)
        {
            checkPropExistence(key, PropType.Int, true);

            integers[key] = value;
        }

        public void SetBool(string key, bool value)
        {
            checkPropExistence(key, PropType.Bool, true);

            booleans[key] = value;
        }

        public void SetString(string key, string value)
        {
            checkPropExistence(key, PropType.String, true);

            strings[key] = value;
        }

        private void checkPropExistence(string key, PropType type, bool shouldExist)
        {
            if (type == PropType.Int)
            {
                if (shouldExist)
                {
                    if (!integers.ContainsKey(key))
                        throw new ArgumentException(nameof(key));
                }
                else
                {
                    if (integers.ContainsKey(key))
                        throw new ArgumentException(nameof(key));
                }
            }
            else if (type == PropType.Bool)
            {
                if (shouldExist)
                {
                    if (!booleans.ContainsKey(key))
                        throw new ArgumentException(nameof(key));
                }
                else
                {
                    if (booleans.ContainsKey(key))
                        throw new ArgumentException(nameof(key));
                }
            }
            else if (type == PropType.String)
            {
                if (shouldExist)
                {
                    if (!strings.ContainsKey(key))
                        throw new ArgumentException(nameof(key));
                }
                else
                {
                    if (strings.ContainsKey(key))
                        throw new ArgumentException(nameof(key));
                }
            }
            else
            {
                throw new ArgumentException(nameof(type));
            }
        }

        public Save ToSave() =>
            new Save(GetType().Name)
                .WithDictionary(nameof(integers), integers)
                .WithDictionary(nameof(booleans), booleans)
                .WithDictionary(nameof(strings), strings);

        public void Load(Save save)
        {
            integers = save.GetIntDictionary(nameof(integers));
            booleans = save.GetBoolDictionary(nameof(booleans));
            strings = save.GetStringDictionary(nameof(strings));
        }

        public object Clone()
        {
            var clone = new Props();

            clone.integers = integers.ToDictionary(x => x.Key, x => x.Value);
            clone.booleans = booleans.ToDictionary(x => x.Key, x => x.Value);
            clone.strings = strings.ToDictionary(x => x.Key, x => "" + x.Value);

            return clone;
        }

        private enum PropType
        {
            Int,
            Bool,
            String
        }
    }
}
