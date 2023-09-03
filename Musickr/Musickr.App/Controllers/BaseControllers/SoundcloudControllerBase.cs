using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Musickr.App.Helpers;

namespace Musickr.App.Controllers.BaseControllers;

public abstract class SoundcloudControllerBase : ControllerBase
{
    public IConfiguration _configuration;
    
    public string? ClientId => _configuration["ClientId"];

    public SoundcloudControllerBase(IConfiguration config)
    {
        _configuration = config;
    }
    
    public async Task<JsonNode?> GetSearch(string q = "", string? place = null)
    {
        if (ClientId is null)
            return null;
        
        using var client = new HttpClient();
        
        var parameters = new List<KeyValuePair<string, string>>
        {
            new ("q", q),
            new ("facet", "place"),
            new ("client_id", ClientId),
            new ("limit", "20")
        };
        
        if (place is not null)
            parameters.Add(new KeyValuePair<string, string>("filter.place", place));

        var apiResult = await HttpClientHelper.GetWithParametersAsync(
            uri: "https://api-v2.soundcloud.com/search/users?",
            parameters: parameters,
            httpClient: client
        );
        
        return JsonNode.Parse(apiResult);
    }
    
    public async Task<JsonNode?> GetUserTracks(int userId)
    {
        if (ClientId is null)
            return null;
        
        using var client = new HttpClient();
        
        var parameters = new List<KeyValuePair<string, string>>
        {
            new ("client_id", ClientId)
        };
        
        var apiResult = await HttpClientHelper.GetWithParametersAsync(
            uri: $"https://api-v2.soundcloud.com/users/{userId}/tracks?",
            parameters: parameters,
            httpClient: client
        );
        
        return JsonNode.Parse(apiResult);
    }
}