using LIT.Smabu.Shared.Identity;

namespace LIT.Smabu.Server.Services
{
    public class CurrentUser : ICurrentUser
    {
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor?.HttpContext?.User;
            this.Id = user?.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? "?";
            this.Name = user?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "?";
        }

        public string Id { get; }
        public string Name { get; }
    }
}
