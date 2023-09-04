using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Musickr.App.Helpers;

namespace Musickr.App.Controllers.BaseControllers;

public abstract class FlickrControllerBase : ControllerBase
{
    public IConfiguration _configuration;
    
    public string? ApiKey => _configuration["FlickrApiKey"];

    public FlickrControllerBase(IConfiguration config)
    {
        _configuration = config;
    }
    
    public async Task<JsonNode?> GetSearch(string q)
    {
        if (ApiKey is null)
            return null;
        
        using var client = new HttpClient();
        
        var parameters = new List<KeyValuePair<string, string>>
        {
            new ("text", q),
            new ("api_key", ApiKey),
            new ("format", "json"),
            new ("nojsoncallback", "1"),
            new ("method", "flickr.photos.search"),
            new ("sort", "interestingness-desc"),
            new ("extras", "owner_name"),
            new ("content_types", "0")
        };

        var apiResult = await HttpClientHelper.GetWithParametersAsync(
            uri: "https://www.flickr.com/services/rest/?",
            parameters: parameters,
            httpClient: client
        );
        
        return JsonNode.Parse(apiResult);
    }

    public string GetPhotoUrl(string serverId, string photoId, string secret, string size)
        => $"https://live.staticflickr.com/{serverId}/{photoId}_{secret}_{size}.jpg";
}