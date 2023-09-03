using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Musickr.App.Controllers.BaseControllers;
using Musickr.App.Helpers;
using Musickr.App.Models;

namespace Musickr.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TracksController : SoundcloudControllerBase
{
    private ILogger<TracksController> _logger;
    
    public TracksController(ILogger<TracksController> logger, IConfiguration config) : base(config)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Track>> Get([FromQuery] string place = "")
    {
        var tracks = new List<Track>();
        
        using var client = new HttpClient();
        
        var search = await GetSearch(
            q: place, 
            place: place
        );

        if (search is null)
            return tracks;
        
        // Get users collection
        var users = search["collection"].AsArray();

        // Get users tracks (15 users max for the moment)
        var usersTracksTasks = users.Take(15).Select((user) =>
        {
            int userId = (int)user["id"];
            return GetUserTracks(userId);
        });

        await Task.WhenAll(usersTracksTasks);

        var usersTracks = usersTracksTasks.Select((response) => response.Result);
        
        foreach (var jsonObject in usersTracks)
        {
            // Get user tracks collection
            var userTracks = jsonObject["collection"]
                .AsArray()
                .Take(3); // 3 Tracks per artist max

            foreach (var track in userTracks)
            {
                tracks.Add(new Track()
                {
                    Author = (string)track["user"]["username"],
                    Title = (string)track["title"],
                    Url = (string)track["permalink_url"]
                });
            }
        }
        
        // Shuffle list !
        var random = new Random();
        var shuffledTracks = tracks.OrderBy(_ => random.Next()).ToList();
        
        return shuffledTracks;
    }
}