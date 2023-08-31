using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Musickr.App.Models;

namespace Musickr.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TracksController : ControllerBase
{
    private ILogger<TracksController> _logger;
    private IConfiguration _configuration;
    
    public TracksController(ILogger<TracksController> logger, IConfiguration config)
    {
        _logger = logger;
        _configuration = config;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Track>> Get([FromQuery] string place = "")
    {
        using var client = new HttpClient();

        var clientId = _configuration["ClientId"];
        
        var parameters = new List<KeyValuePair<string, string>>
        {
            new ("q", place),
            new ("filter.place", place),
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
            return new List<Track>();
        }
        
        // Get users collection
        var users = jsonObject["collection"].AsArray();

        // Get users tracks (15 users max for the moment)
        var responsesTask = users.Take(15).Select((user) =>
        {
            int userId = (int)user["id"];
            
            parameters = new List<KeyValuePair<string, string>>
            {
                new ("client_id", clientId)
            };
            urlWithParams = QueryHelpers.AddQueryString($"https://api-v2.soundcloud.com/users/{userId}/tracks?", parameters);
        
            return client.GetAsync(urlWithParams);
        });

        await Task.WhenAll(responsesTask);

        var responses = responsesTask.Select((response) => response.Result);

        var musics = new List<Track>();
        foreach (var response in responses)
        {
            stringResult = await response.Content.ReadAsStringAsync();
        
            jsonObject = JsonNode.Parse(stringResult);

            if (jsonObject is null)
            {
                continue;
            }
        
            // Get users collection
            var tracks = jsonObject["collection"]
                .AsArray()
                .Take(3); // 3 Tracks per artist max

            foreach (var track in tracks)
            {
                musics.Add(new Track()
                {
                    Author = (string)track["user"]["username"],
                    Title = (string)track["title"],
                    Url = (string)track["permalink_url"]
                });
            }
        }
        
        // Shuffle list !
        Random rnd = new Random();
        var shuffledTracks = musics.OrderBy(_ => rnd.Next()).ToList();
        
        return shuffledTracks;
    }
}