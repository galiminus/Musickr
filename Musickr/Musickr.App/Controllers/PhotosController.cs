using Microsoft.AspNetCore.Mvc;
using Musickr.App.Controllers.BaseControllers;
using Musickr.App.Models;

namespace Musickr.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : FlickrControllerBase
{
    private ILogger<PhotosController> _logger;
    
    public PhotosController(ILogger<PhotosController> logger, IConfiguration config) : base(config)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Photo>> Get([FromQuery] string q = "")
    {
        var search = await GetSearch(q: q);

        if (search is null)
        {
            return new List<Photo>();
        }
        
        var photos = search["photos"]["photo"].AsArray();

        return photos.Select(photo => new Photo()
        {
            Title = (string)photo["title"], 
            Url = GetPhotoUrl(
                serverId: (string)photo["server"], 
                photoId: (string)photo["id"], 
                secret: (string)photo["secret"], 
                size: "b"
            ),
            Author = (string)photo["ownername"]
        }).ToList();
    }
}