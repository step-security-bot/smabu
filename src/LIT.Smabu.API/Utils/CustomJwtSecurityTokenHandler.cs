using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

internal class CustomJwtSecurityTokenHandler : JsonWebTokenHandler
{
    public override SecurityToken ReadToken(string token)
    {
        var result = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token);
        return result;
    }
}