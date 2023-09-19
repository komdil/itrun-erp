using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace Warehouse.Client.Auth
{
    public class WarehouseAuthStateProvider : AuthenticationStateProvider
    {
        public static bool IsAuthenticated { get; set; }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal user;
            if (IsAuthenticated)
            {
                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3ByaW1hcnlzaWQiOiI2YTU1MWQ4ZS04ZWQ1LTQ3NTUtYTA3ZC1hYTUzNDdkNzljYjYiLCJleHAiOjE2OTUxNTE1MTIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjYxOTU1IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDIwMCJ9.g4L6j8e6PsYttpf7kfAu-3ibluS01MBb9GwyGL1YPOs";
                var claims = ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwt");
                user = new ClaimsPrincipal(identity);
            }
            else
            {
                user = new ClaimsPrincipal();
            }

            var state = new AuthenticationState(user);
            var result = Task.FromResult(state);
            NotifyAuthenticationStateChanged(result);
            return result;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
