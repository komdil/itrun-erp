using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Contracts.User.Responses
{
    public class JwtTokenResponse
    {
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("RefreshToken")]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenValidTo { get; set; }
    }
}
