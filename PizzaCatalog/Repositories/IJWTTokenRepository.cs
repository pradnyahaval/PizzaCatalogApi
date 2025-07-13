using Microsoft.AspNetCore.Identity;

namespace PizzaCatalog.WebApi.Repositories
{
    public interface IJWTTokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
