using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Musickr.App.Models;

namespace Musickr.App.Controllers;

[ApiController]
[Route("[controller]")]
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
        var result = 
            await client.GetAsync($"https://api-v2.soundcloud.com/search/users?q={q}&variant_ids=&facet=place&client_id={clientId}&limit=20");

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