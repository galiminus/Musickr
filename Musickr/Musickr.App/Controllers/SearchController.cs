using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Musickr.App.Models;

namespace Musickr.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private ILogger<SearchController> _logger;
    private IConfiguration _configuration;
    
    public SearchController(ILogger<SearchController> logger, IConfiguration config)
    {
        _logger = logger;
        _configuration = config;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Place>> Get([FromQuery] string q = "")
    {
        using var client = new HttpClient();

        var clientId = _configuration["ClientId"];
        
        var parameters = new List<KeyValuePair<string, string>>
        {
            new ("q", q),
            new ("facet", "place"),
            new ("client_id", clientId),
            new ("limit", "20")
        };
        var urlWithParams = QueryHelpers.AddQueryString("https://api-v2.soundcloud.com/search/users?", parameters);
        
        var result = 
            await client.GetAsync(urlWithParams);

        var stringResult = await result.Content.ReadAsStringAsync();
        
        var jsonObject = JsonNode.Parse(stringResult);

        if (jsonObject is null)
        {
            return new List<Place>();
        }
        
        var facets = jsonObject["facets"][0]["facets"].AsArray();

        return facets.Select(facet => new Place()
        {
            Name = (string)facet["value"], 
            UserCount = (int)facet["count"]
        }).ToList();
    }
}