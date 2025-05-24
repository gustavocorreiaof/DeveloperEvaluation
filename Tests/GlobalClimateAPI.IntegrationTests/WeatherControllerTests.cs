using FluentAssertions;
using GlobalClimateAPI.IntegrationTests.Factories;
using System.Net;

namespace GlobalClimateAPI.IntegrationTests;

public class WeatherControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public WeatherControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWeather_ReturnsSuccess_WhenCityExists()
    {
        // Arrange
        var city = "Sao Paulo";

        // Act
        var response = await _client.GetAsync($"/weather?city={city}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}