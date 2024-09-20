using System.Net.Http.Headers;
using media_api.Middleware;
using media_api.Models.Plex.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace media_api.Controllers.Plex.Core;

public class PlexCoreController : Controller
{
    #region Methods

    public PlexCoreController()
    {
        this._Env = new EnvMiddleware();
    }

    private HttpClient Client
    {
        get
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            client.DefaultRequestHeaders.Add("X-Plex-Token", this._Env.reader["PLEX_TOKEN"]);
            return client;
        }
    }

    protected async Task<bool> IsLibraryType(string libraryType, int libraryId)
    {
        string rawLibrary = await this.PlexRequest("/library/sections");
        if ("" == rawLibrary)
        {
            return false;
        }

        LibrariesResponse? json = JsonConvert.DeserializeObject<LibrariesResponse>(rawLibrary);
        if (null == json)
        {
            return false;
        }

        LibraryListing[]? directory = json.MediaContainer.Directory;

        if (null == directory)
        {
            return false;
        }

        IEnumerable<LibraryListing> result = directory.Where(lib =>
        {
            return (lib.Location[0].id == libraryId && lib.type == libraryType);
        });

        if (1 != result.Count())
        {
            return false;
        }

        return true;
    }

    protected async Task<string> PlexRequest(string route)
    {
        HttpResponseMessage response = await this.Client.GetAsync($"{this.PlexUrl}{route}");
        return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : "";
    }

    protected async Task<FileContentResult> PlexFileRequest(string route)
    {
        HttpResponseMessage response = await this.Client.GetAsync($"{this.PlexUrl}{route}");
        byte[] contentBytes = await response.Content.ReadAsByteArrayAsync();
        string contentType = response.Content.Headers.ContentType.ToString();
        FileContentResult file = File(contentBytes, contentType);
		return file;
		/* string base64Contents = Convert.ToBase64String(contentBytes); */
		/* return $"data:{contentType};base64,{base64Contents}"; */
    }

    private String PlexUrl
    {
        get
        {
            string requestType = this.UseSSL ? "https" : "http";
            return $"{requestType}://{this._Env.reader["PLEX_ADDRESS"]}:{this._Env.reader["PLEX_PORT"]}";
        }
    }

    private bool UseSSL
    {
        get
        {
            // TODO: Add login to handle if using http or https
            return false;
        }
    }

    #endregion

    #region Params

    protected EnvMiddleware _Env;

    #endregion
}
