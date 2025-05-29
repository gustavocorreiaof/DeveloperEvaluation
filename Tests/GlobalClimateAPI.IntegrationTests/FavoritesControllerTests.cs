using GlobalClimateAPI.Requests;
using GlobalClimateAPI.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GlobalClimateAPI.IntegrationTests;

[TestFixture]
public class FavoritesControllerTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private string token;
    private readonly string userName = "GustavoTest";
    private readonly string password = "StrongPassoword123//";
    private readonly string favoritesUrl = "http://localhost:5233/api/Favorites/";

    [SetUp]
    public async Task SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        token = await GetJWTToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Test]
    public async Task GetFavoriteCitiesByUserId_ReturnsOkResult_WithListOfCities()
    {
        //Arrange and Act
        var response = await _client.GetAsync(favoritesUrl + "GetFavoriteCitiesByUserId?userId=1");
        string responseContent = await response.Content.ReadAsStringAsync();

        var citiesResponse = JsonSerializer.Deserialize<GetFavoriteCitiesResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(citiesResponse?.Cities, Is.Not.Empty);
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    private async Task<string> GetJWTToken()
    {
        LoginRequest request = new LoginRequest() { UserName = userName, Password = password };

        var response = await _client.PostAsJsonAsync("/api/Auth", request);

        var rawJson = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(rawJson);
        return json.RootElement.GetProperty("token").GetString()!;
    }
}