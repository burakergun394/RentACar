using Microsoft.IdentityModel.Tokens;

namespace RentACar.Core.Utilities.Securities.Encryption
{
    public static class SignInCredentialsHelper
    {
        public static SigningCredentials CreateSignInCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}