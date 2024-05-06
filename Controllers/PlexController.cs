using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using DotEnv.Core;
using Newtonsoft.Json;
using media_api.Models;

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

	[HttpGet("/plex/accounts")]
	public async Task<IActionResult> GetAccounts()
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/accounts";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var json = JsonConvert.DeserializeObject<AccountsResponse>(content);
				return Ok(json);
			}
			else
			{
				return BadRequest("Error in retrieving the plex accounts!");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"ERROR: {e.Message}");
		}
	}

	[HttpGet("/plex/account/{AccountId}")]
	public async Task<IActionResult> GetAccount(int AccountId)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/accounts/{AccountId}";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var json = JsonConvert.DeserializeObject<AccountsResponse>(content);
				return Ok(json);
			}
			else
			{
				return BadRequest($"Error in retrieving accouint details for Account ID: {AccountId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"ERROR: {e.Message}");
		}
	}

	[HttpGet("/plex/libraries")]
	public async Task<IActionResult> GetLbraries()
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/sections";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var json = JsonConvert.DeserializeObject<LibrariesResponse>(content);
				return Ok(json);
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

	[HttpGet("/plex/movies/{LibraryId}")]
	public async Task<IActionResult> MovieLibrary(int LibraryId)
	{
		// TODO: Add check to see if library id is for movies
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/sections/{LibraryId}/all";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));

				/* var json = JsonConvert.DeserializeObject<LibraryResponse>(content); */
				/* return Ok(json); */
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

	[HttpGet("/plex/movies/{LibraryId}/recently/released")]
	public async Task<IActionResult> ResentMovieReleased(int LibraryId)
	{
		// TODO: Add check to see if library id is for movies
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
				return BadRequest($"Error retrieving recently released movies for library id: {LibraryId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{LibraryId}/recently/added")]
	public async Task<IActionResult> ResentMovieAdded(int LibraryId)
	{
		// TODO: Add check to see if library id is for movies
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/sections/{LibraryId}/recentlyAdded";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
			}
			else
			{
				return BadRequest($"Error retrieving recently added movies for library id: {LibraryId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movie/{MovieId}")]
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

	[HttpGet("/plex/image/poster/{FullPath}")]
	public async Task<IActionResult> GetPoster(string FullPath)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/{FullPath}";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsByteArrayAsync();
				Console.WriteLine(content);
				return Ok(content);
			}
			else
			{
				return BadRequest($"Failed to retrieve the poster located at: {FullPath}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/image/background/{FullPath}")]
	public async Task<IActionResult> GetBackground(string FullPath)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/{FullPath}";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsByteArrayAsync();
				Console.WriteLine(content);
				return Ok(content);
			}
			else
			{
				return BadRequest($"Failed to retrieve the background located at: {FullPath}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/shows/{LibraryId}")]
	public async Task<IActionResult> ShowLibrary(int LibraryId)
	{
		// TODO: Add check to see if library id is for shows
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/sections/{LibraryId}/all";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));

				/* var json = JsonConvert.DeserializeObject<LibraryResponse>(content); */
				/* return Ok(json); */
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

	[HttpGet("/plex/show/{ShowId}/seasons")]
	public async Task<IActionResult> GetShowSeasons(int ShowId)
	{
		try
		{
			var client = this.Plex_configure();
			var endPoint = $"{this.PlexUrl()}/library/metadata/{ShowId}/children";
			var response = await client.GetAsync(endPoint);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
			}
			else
			{
				return BadRequest($"Failed to retrieve the seasons for Show ID: {ShowId}");
			}
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}
}

