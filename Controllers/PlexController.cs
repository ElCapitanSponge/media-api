using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using media_api.Models;

namespace media_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlexController : PlexCoreController
{
	# region Methods

	public PlexController()
	{
	}

	/**
	 * The following api endpoints are extrapolated from:
	 * https://www.plexopedia.com/plex-media-server/api/
	 */

	[HttpGet("/plex/account/{AccountId}")]
	public async Task<IActionResult> Account(int AccountId)
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

	[HttpGet("/plex/accounts")]
	public async Task<IActionResult> Accounts()
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

	[HttpGet("/plex/image/background/{FullPath}")]
	public async Task<IActionResult> Background(string FullPath)
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

	[HttpGet("/plex/libraries")]
	public async Task<IActionResult> Libraries()
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

	[HttpGet("/plex/movie/{MovieId}")]
	public async Task<IActionResult> Movie(int MovieId)
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
			if ("" == content)
			{
				return BadRequest("Error retrieving the movie lib information");
			}
			var json = JsonConvert.DeserializeObject<MovieLibraryResponse>(content);
			return Ok(json);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/image/poster/{FullPath}")]
	public async Task<IActionResult> Poster(string FullPath)
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

	[HttpGet("/plex/movies/{LibraryId}/recently/added")]
	public async Task<IActionResult> ResentMoviesAdded(int LibraryId)
	{
		bool valid = await this.IsLibraryType(LibraryTypes.MOVIE, LibraryId);

		if (false == valid)
		{
			return BadRequest($"Desired library id {LibraryId} is not a movie library!");
		}

		try
		{
			string content = await this.PlexRequest($"/library/sections/{LibraryId}/recentlyAdded");
			if ("" == content)
			{
				return BadRequest("Error retrieving recently added movies");
			}
			var json = JsonConvert.DeserializeObject<MovieLibraryResponse>(content);
			return Ok(json);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/movies/{LibraryId}/recently/released")]
	public async Task<IActionResult> ResentMoviesReleased(int LibraryId)
	{
		bool valid = await this.IsLibraryType(LibraryTypes.MOVIE, LibraryId);

		if (false == valid)
		{
			return BadRequest($"Desired library id {LibraryId} is not a movie library!");
		}

		try
		{
			string content = await this.PlexRequest($"/library/sections/{LibraryId}/newest");
			if ("" == content)
			{
				return BadRequest("Error retrieving recently released movies");
			}
			var json = JsonConvert.DeserializeObject<MovieLibraryResponse>(content);
			return Ok(json);
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
			if ("" == content)
			{
				return BadRequest("Error retrieving the show collection");
			}
			var json = JsonConvert.DeserializeObject<ShowLibraryResponse>(content);
			return Ok(json);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Error: {e.Message}");
		}
	}

	[HttpGet("/plex/show/{ShowId}/seasons")]
	public async Task<IActionResult> ShowSeasons(int ShowId)
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

	#endregion
}

