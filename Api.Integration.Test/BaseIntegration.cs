using Api.Application;
using Api.Data.Context;
using Api.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Api.Integration.Test;

public class BaseIntegration : IDisposable
{
    public MyContext MyContext { get; set; }
    public IMapper Mapper { get; set; }

    private const string _baseUrl = "http://localhost:7042";

    private readonly HttpClient _client;

    public BaseIntegration()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<MyContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    MyContext = scopedServices.GetRequiredService<MyContext>();
                    MyContext.Database.EnsureCreated();
                    Mapper = scopedServices.GetRequiredService<IMapper>();
                });
            });

        _client = factory.CreateClient();
    }    
    public async Task<HttpResponseMessage> PostJsonAsync(string url, object dataClass)
    {
        await AdicionarToken();
        return await _client.PostAsync($"{_baseUrl}/{url}",
            new StringContent(JsonSerializer.Serialize(dataClass), System.Text.Encoding.UTF8,
            "application/json"));
    }
    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        await AdicionarToken();
        return await _client.GetAsync(url);
    }
    public async Task<HttpResponseMessage> PutAsync(string url, object dataClass)
    {
        await AdicionarToken();
        return await _client.PutAsync(url,
            new StringContent(JsonSerializer.Serialize(dataClass), System.Text.Encoding.UTF8,
            "application/json"));
    }
    private async Task AdicionarToken()
    {
        LoginDto loginDto = new()
        {
            Email = "dev.caiosampaio@outlook.com"
        };

        var resultJson = await _client
            .PostAsync($"{_baseUrl}/login",
                        new StringContent(JsonSerializer.Serialize(loginDto),
                        System.Text.Encoding.UTF8,
                         "application/json"));
        var jsonLogin = await resultJson.Content.ReadAsStringAsync();
        var loginObject = JsonSerializer.Deserialize<LoginResponseDto>(jsonLogin)!;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                                                   loginObject.accessToken);
    }
    public void Dispose()
    {
        MyContext.Dispose();
        _client.Dispose();
    }
}
