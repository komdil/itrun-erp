using Account.Api.Contracts.Requests;
using Account.Api.Contracts.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class AccountServiceTests
    {
        private HttpClient _httpClient;

        public AccountServiceTests()
        {
            var webApp = new WebApplicationFactory<Program>();
            _httpClient = webApp.CreateClient();
        }

        [Test]
        public async Task Account_SignIn_ShouldReturnToken_WhenUserNameAndPasswordAreCorrectTest()
        {
            //Arrange
            AccountLoginRequest loginRequest = new AccountLoginRequest()
            {
                Username = "dilshod",
                Password = "1234"
            };
            SaveUserToDatabase(loginRequest);
            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("auth/sign-in", content);

            //Assert
            response.EnsureSuccessStatusCode();
            string jsonContentFromResponse = await response.Content.ReadAsStringAsync();
            AccountLoginResponse accountLoginResponse = JsonConvert.DeserializeObject<AccountLoginResponse>(jsonContentFromResponse);

            Assert.That(accountLoginResponse.Success, Is.True);
            Assert.That(accountLoginResponse.AccessToken, Is.Not.Null.Or.Empty);
        }

        private void SaveUserToDatabase(AccountLoginRequest loginRequest)
        {
            //TODO
        }
    }
}
