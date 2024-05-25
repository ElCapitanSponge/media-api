using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using media_api.Models;

namespace media_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlexController : PlexCoreController
{
	public PlexController()
	{
	}

	/**
	 * The following api endpoints are extrapolated from:
	 * https://www.plexopedia.com/plex-media-server/api/
	 */

	[HttpGet("/plex/accounts")]
	public async Task<IActionResult> GetAccounts()
	{
		try
		{
			string content = await this.PlexRequest("/accounts");
			AccountsResponse? json = JsonConvert.DeserializeObject<AccountsResponse>(content);
			return Ok(json);
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
			string content = await this.PlexRequest($"/accounts/{AccountId}");
			var json = JsonConvert.DeserializeObject<AccountsResponse>(content);
			return Ok(json);
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
			string content = await this.PlexRequest("/library/sections");
			if ("" == content)
			{
				return BadRequest("Error retrieving plex library information!");
			}
			var json = JsonConvert.DeserializeObject<LibrariesResponse>(content);
			return Ok(json);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{LibraryId}")]
	public async Task<IActionResult> MovieLibrary(int LibraryId)
	{
		bool valid = await this.IsLibraryType(LibraryTypes.MOVIE, LibraryId);

		if (false == valid)
		{
			return BadRequest($"Desired library id {LibraryId} is not a movie library!");
		}

		try
		{
			string content = await this.PlexRequest($"/library/sections/{LibraryId}/all");
			return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
			/* return Ok(json); */
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{LibraryId}/recently/released")]
	public async Task<IActionResult> ResentMovieReleased(int LibraryId)
	{
		bool valid = await this.IsLibraryType(LibraryTypes.MOVIE, LibraryId);

		if (false == valid)
		{
			return BadRequest($"Desired library id {LibraryId} is not a movie library!");
		}

		try
		{
			string content = await this.PlexRequest($"/library/sections/{LibraryId}/newest");
			return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{LibraryId}/recently/added")]
	public async Task<IActionResult> ResentMovieAdded(int LibraryId)
	{
		bool valid = await this.IsLibraryType(LibraryTypes.MOVIE, LibraryId);

		if (false == valid)
		{
			return BadRequest($"Desired library id {LibraryId} is not a movie library!");
		}

		try
		{
			string content = await this.PlexRequest($"/library/sections/{LibraryId}/recentlyAdded");
			return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
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
			string content = await this.PlexRequest($"/library/metadata/{MovieId}");
			return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
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
			byte[] content = await this.PlexByteRequest($"/{FullPath}");
			Console.WriteLine(content);
			return Ok(content);
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
			byte[] content = await this.PlexByteRequest($"/{FullPath}");
			Console.WriteLine(content);
			return Ok(content);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/shows/{LibraryId}")]
	public async Task<IActionResult> ShowLibrary(int LibraryId)
	{
		bool valid = await this.IsLibraryType(LibraryTypes.SHOW, LibraryId);

		if (false == valid)
		{
			return BadRequest($"Desired library id {LibraryId} is not a show library!");
		}

		try
		{
			string content = await this.PlexRequest($"/library/sections/{LibraryId}/all");
			return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
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
			string content = await this.PlexRequest($"/library/metadata/{ShowId}/children");
			return Ok(JsonConvert.SerializeObject(content, Formatting.Indented));
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}
}

