using media_api.Models.Plex.Movies;
using media_api.Models.Plex.Shows;

namespace media_api.Models.Plex.Core;

// INFO: String like enums
public static class LibraryTypes
{
    public static readonly string MOVIE = "movie";
    public static readonly string SHOW = "show";
}

// INFO: General
public class CoreWrapper
{
    public required int size { get; set; }
    public string? identifier { get; set; }
    public required bool allowSync { get; set; }
    public string? mediaTagPrefix { get; set; }
    public object? mediaTagVersion { get; set; }
    public string? title1 { get; set; }
    public string? title2 { get; set; }
}

public class Location
{
    public int id { get; set; }
    public string? path { get; set; }
}

// INFO: Libraries
public class LibrariesWrapper : CoreWrapper
{
    public LibraryListing[]? Directory { get; set; }
}

public class LibrariesResponse
{
    public required LibrariesWrapper MediaContainer { get; set; }
}

public class LibraryListing
{
    public required bool allowSync { get; set; }
    public required string art { get; set; }
    public required string composite { get; set; }
    public required bool filters { get; set; }
    public required bool refreshing { get; set; }
    public required string thumb { get; set; }
    public required string key { get; set; }
    public required string type { get; set; }
    public required string title { get; set; }
    public required string agent { get; set; }
    public required string scanner { get; set; }
    public required string language { get; set; }
    public required string uuid { get; set; }
    public required object updatedAt { get; set; }
    public required object createdAt { get; set; }
    public required object scannedAt { get; set; }
    public required bool content { get; set; }
    public required bool directory { get; set; }
    public required object contentChangedAt { get; set; }
    public required bool hidden { get; set; }
    public required Location[] Location { get; set; }
}

// INFO: Library
public class LibraryCoreWrapper : CoreWrapper
{
    public required int librarySectionID { get; set; }
    public required string librarySectionTitle { get; set; }
    public required string librarySectionUUID { get; set; }
    public required string thumb { get; set; }
    public required string viewGroup { get; set; }
}

public class MovieLibrary : LibraryCoreWrapper
{
    public required MoviesBase[] metadata { get; set; }
}

public class MovieLibraryResponse
{
    public required MovieLibrary MediaContainer { get; set; }
}

public class ShowLibrary : LibraryCoreWrapper
{
    public required ShowsBase[] metadata { get; set; }
}

public class ShowLibraryResponse
{
    public required ShowLibrary MediaContainer { get; set; }
}

public class IdStringItem
{
	public required string id { get; set; }
}

public class RatingItem
{
	public required string image { get; set; }
	public required decimal rating { get; set; }
	public required string type { get; set; }
}

public class TagItem
{
    public required string tag { get; set; }
    public int? id { get; set; }
    public string? filter { get; set; }
}

public class ExtendedTagItem : TagItem
{
	public required string thumb { get; set; }
	public required string tagKey { get; set; }
	public string? role { get; set; }
}

public class Media
{
    public required int id { get; set; }
    public required int duration { get; set; }
    public required int bitrate { get; set; }
    public required int width { get; set; }
    public required int height { get; set; }
    public required decimal aspectRatio { get; set; }
    public required int audioChannels { get; set; }
    public required string audioCodec { get; set; }
    public required string videoCodec { get; set; }
    public required string videoResolution { get; set; }
    public required string container { get; set; }
    public required string videoFrameRate { get; set; }
    public required bool optimizedForStreaming { get; set; }
    public required string audioProfile { get; set; }
    public required bool has64bitOffsets { get; set; }
    public required string videoProfile { get; set; }
    public required MediaPart[] part { get; set; }
}

public class MediaPart
{
    public required int id { get; set; }
    public required string key { get; set; }
    public required int duration { get; set; }
    public required string file { get; set; }
    public required string audioProfile { get; set; }
    public required string container { get; set; }
    public required bool has64bitOffsets { get; set; }
    public required bool optimizedForStreaming { get; set; }
    public required string videoProfile { get; set; }
    public Stream[]? stream { get; set; }
}

public class Stream
{
    // TODO: add the elements for a stream object
}

public class MediaCommon
{
    public required int ratingKey { get; set; }
    public required string key { get; set; }
    public required string guid { get; set; }
    public required string slug { get; set; }
    public required string studio { get; set; }
    // TODO: convert the type to an enum
    public required string type { get; set; }
    public required string title { get; set; }
    public required string contentRating { get; set; }
    public required string summary { get; set; }
    public decimal? rating { get; set; }
    public required decimal audienceRating { get; set; }
    public required int lastViewedAt { get; set; }
    public required int year { get; set; }
    public required string tagline { get; set; }
    public required string thumb { get; set; }
    public required string art { get; set; }
    public required int duration { get; set; }
    public required DateTime originallyAvailableAt { get; set; }
    public required int addedAt { get; set; }
    public required int updatedAt { get; set; }
    public required Media[] media { get; set; }
    public required string audienceRatingImage { get; set; }
    public string? ratingImage { get; set; }
}
