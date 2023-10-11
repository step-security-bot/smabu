namespace LIT.Smabu.Infrastructure.Cache
{
    public interface IUserCache
    {
        void AddOrUpdate(string userId, string group, object key, object value);
        T? Get<T>(string userId, string group, object key) where T : class;
        void Remove(string userId, string group, object key);
        bool ContainsKey(string userId, string group, object key);
        IEnumerable<T> GetValues<T>(string userId, string group) where T : class;
    }
}