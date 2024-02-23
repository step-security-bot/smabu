namespace LIT.Smabu.Shared.Identity
{
    public interface ICurrentUser
    {
        string Id { get; }
        string Name { get; }
    }
}
