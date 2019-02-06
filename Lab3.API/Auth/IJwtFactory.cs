using System.Security.Claims;
using System.Threading.Tasks;

namespace Lab3.API.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, string role);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
