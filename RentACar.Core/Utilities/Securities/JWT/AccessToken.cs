using System;

namespace RentACar.Core.Utilities.Securities.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}