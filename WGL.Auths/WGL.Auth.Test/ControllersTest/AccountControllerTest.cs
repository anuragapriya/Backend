using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WGL.Auth.Application.DTOs.Account;
using WGL.Auth.Test.CustomFactory;

namespace WGL.Auth.Test.ControllersTest
{
    public class AccountControllerTest : IDisposable
    {
        private CustomWebApiFactory _customWebApiFactory;
        private HttpClient _httpClient;
        public AccountControllerTest()
        {
            _customWebApiFactory = new CustomWebApiFactory();
            _httpClient = _customWebApiFactory.CreateClient();
        }

        public void Dispose()
        {
            _customWebApiFactory.Dispose();
            _httpClient.Dispose();
        }

        [Fact]
        public async Task GenerateTokenTest()
        {
            var mockTokenResponse = new GenerateTokenResponse
            {
                access_token= "c7fed62219ce4e4ca027649bd726ca53",
                account_id=1010,created_at=DateAndTime.Now,
                expires_in=36000,
                refresh_token= "2174bf99375c45e3a01343913d3f5dca",
                token_type = "bearer" 
            };

            _customWebApiFactory.AccountRepositoryMock.Setup(r => r.GenerateTokenQuery()).ReturnsAsync(mockTokenResponse);
            var response = await _httpClient.GetAsync("api/Account/GenerateToken");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //var data = JsonConvert.DeserializeObject<GenerateTokenResponse> (await response.Content.ReadAsStringAsync());
            //Assert.Equal("c7fed62219ce4e4ca027649bd726ca53", data.access_token);
        }
    }
}
