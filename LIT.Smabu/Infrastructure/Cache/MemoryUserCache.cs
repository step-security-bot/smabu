namespace LIT.Smabu.Infrastructure.Cache
{
    public class MemoryUserCache : IUserCache
    {
        private readonly Dictionary<(string userId, string group), Dictionary<object, object>> cache = new();

        public void AddOrUpdate(string userId, string group, object key, object value)
        {
            CheckFirstLevel(userId, group);
            if (!cache[(userId, group)].ContainsKey(key))
            {
                cache[(userId, group)].Add(key, value);
            }
            else
            {
                cache[(userId, group)][key] = value;
            }
        }

        public T? Get<T>(string userId, string group, object key) where T : class
        {
            if (CheckFirstLevel(userId, group, false))
            {
                if (cache[(userId, group)].TryGetValue(key, out var result))
                {
                    return (T)result;
                }
            }
            return null;
        }

        public void Remove(string userId, string group, object key)
        {
            if (CheckFirstLevel(userId, group, false))
            {
                cache[(userId, group)].Remove(key);
            }
        }

        public IEnumerable<T> GetValues<T>(string userId, string group) where T : class
        {
            if (CheckFirstLevel(userId, group, false))
            {
                return cache[(userId, group)].Values.OfType<T>();
            }
            else
            {
                return new List<T>();
            }
        }

        public bool ContainsKey(string userId, string group, object key)
        {
            if (CheckFirstLevel(userId, group, false))
            {
                return cache[(userId, group)].ContainsKey(key);
            }
            else
            {
                return false;
            }
        }

        private bool CheckFirstLevel(string userId, string group, bool add = true)
        {
            if (!cache.ContainsKey((userId, group)))
            {
                if (add)
                {
                    cache.Add((userId, group), new Dictionary<object, object>());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
