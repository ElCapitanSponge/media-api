using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using media_api.Models;
using DotEnv.Core;

namespace media_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlexController : ControllerBase
{
    public EnvReader reader;
    public PlexController()
    {
        new EnvLoader().Load();
        this.reader = new EnvReader();
    }

    private HttpClient Plex_configure()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
        client.DefaultRequestHeaders.Add("X-Plex-Token", this.reader["PLEX_TOKEN"]);

        return client;
    }

    private String PlexUrl()
    {
        return $"http://{this.reader["PLEX_ADDRESS"]}:{this.reader["PLEX_PORT"]}";
    }

    [HttpGet(Name = "GetLibrary")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var client = Plex_configure();
            var response = await client.GetAsync($"{PlexUrl()}/library/sections/");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var library = JsonSerializer.Deserialize<Library>(json);
                return Ok(library);
            }
            else
            {
                return BadRequest("Error retrieving plex library information!");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error: {e.Message}");
        }
    }
}

