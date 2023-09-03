using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Musickr.App.Controllers.BaseControllers;
using Musickr.App.Models;

namespace Musickr.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : SoundcloudControllerBase
{
    private ILogger<SearchController> _logger;
    
    public SearchController(ILogger<SearchController> logger, IConfiguration config) : base(config)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Place>> Get([FromQuery] string q = "")
    {
        var search = await GetSearch(q: q);

        if (search is null)
        {
            return new List<Place>();
        }
        
        var facets = search["facets"][0]["facets"].AsArray();

        return facets.Select(facet => new Place()
        {
            Name = (string)facet["value"], 
            UserCount = (int)facet["count"]
        }).ToList();
    }
}