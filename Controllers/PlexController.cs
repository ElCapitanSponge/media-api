using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using DotEnv.Core;
using Newtonsoft.Json;

namespace media_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlexController : Controller
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

	/**
	 * The following api enpoints are extrapolated from:
	 * https://www.plexopedia.com/plex-media-server/api/
	 */

	[HttpGet("/plex/libraries")]
	public async Task<IActionResult> Lbraries()
	{
		try
		{
			var client = this.Plex_configure();
			var response = await client.GetAsync($"{this.PlexUrl()}/library/sections");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
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

	[HttpGet("/plex/library/{LibraryId}")]
	public async Task<IActionResult> Library(int LibraryId)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/sections/{LibraryId}/all";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
			}
			else
			{
				return BadRequest($"Error retrieving content for library id: {LibraryId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{LibraryId}/recent_release")]
	public async Task<IActionResult> ResentMovieRelease(int LibraryId)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/sections/{LibraryId}/newest";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
			}
			else
			{
				return BadRequest($"Error retrieving recently release movies for library id: {LibraryId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{MovieId}")]
	public async Task<IActionResult> GetMovie(int MovieId)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/metadata/{MovieId}";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
			}
			else
			{
				return BadRequest($"Failed to retrieve information for Movie ID: {MovieId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}
}

