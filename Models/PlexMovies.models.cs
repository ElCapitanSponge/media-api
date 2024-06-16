using media_api.Models.Plex.Core;

namespace media_api.Models.Plex.Movies;

// INFO: Movies
public class MoviesBase : MediaCommon
{
    public required TagItem[] genre { get; set; }
    public required TagItem[] country { get; set; }
    public required TagItem[] director { get; set; }
    public required TagItem[] writer { get; set; }
    public required TagItem[] role { get; set; }
}

public class MovieMediaStream
{
    public required int id { get; set; }
    public required int streamType { get; set; }
    public required bool Default { get; set; }
    public required string codec { get; set; }
    public required int index { get; set; }
    public required int bitrate { get; set; }
    public required string language { get; set; }
    public required string languageTag { get; set; }
    public required string languageCode { get; set; }
    public int? bitDepth { get; set; }
    public string? chromaLocation { get; set; }
    public string? chromaSubsampling { get; set; }
    public int? codedHeight { get; set; }
    public int? codedWidth { get; set; }
    public decimal? frameRate { get; set; }
    public bool? hasScalingMatrix { get; set; }
    public int? height { get; set; }
    public int? level { get; set; }
    public string? profile { get; set; }
    public int? refFrames { get; set; }
    public string? scanType { get; set; }
    public int? width { get; set; }
    public required string displayTitle { get; set; }
    public required string extendedDisplayTitle { get; set; }
    public string? title { get; set; }
    public string? audioChannelLayout { get; set; }
    public int? samplingRate { get; set; }

}

public class MovieMediaPart
{
    public required int id { get; set; }
    public required string key { get; set; }
    public required long duration { get; set; }
    public required string file { get; set; }
    public required long size { get; set; }
    public required string container { get; set; }
    public required string videoProfile { get; set; }
    public MovieMediaStream[]? stream { get; set; }
}

public class MovieMedia
{
    public required int id { get; set; }
    public required long duration { get; set; }
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
    public required string videoProfile { get; set; }
    public required MovieMediaPart[] part { get; set; }
    public required TagItem[] genre { get; set; }
    public required TagItem[] country { get; set; }
    public required IdStringItem[] guid { get; set; }
    public required RatingItem[] rating { get; set; }
    public required ExtendedTagItem[] director { get; set; }
    public required ExtendedTagItem[] writer { get; set; }
    public required ExtendedTagItem[] role { get; set; }
    public required ExtendedTagItem[] producer { get; set; }
}

public class MovieMetadata
{
    public required int ratingKey { get; set; }
    public required string key { get; set; }
    public required string guid { get; set; }
    public required string slug { get; set; }
    public required string studio { get; set; }
    public required string type { get; set; }
    public required string title { get; set; }
    public required string librarySectionTitle { get; set; }
    public required int librarySectionID { get; set; }
    public required string librarySectionKey { get; set; }
    public required string contentRating { get; set; }
    public required string summary { get; set; }
    public required decimal rating { get; set; }
    public required decimal audienceRating { get; set; }
    public required int year { get; set; }
    public string? tagline { get; set; }
    public required string thumb { get; set; }
    public required string art { get; set; }
    public required long duration { get; set; }
    public required int addedAt { get; set; }
    public required int updatedAt { get; set; }
    public required string audienceRatingImage { get; set; }
    public required string chapterSource { get; set; }
    public required string primaryExtraKey { get; set; }
    public required string ratingImage { get; set; }
    public required MovieMedia[] media { get; set; }
    public required TagItem[] genre { get; set; }
    public required TagItem[] country { get; set; }
}

public class Movie
{
    public required int size { get; set; }
    public required bool allowSync { get; set; }
    public required string identifier { get; set; }
    public required int librarySectionID { get; set; }
    public required string librarySectionTitle { get; set; }
    public required string librarySectionUUID { get; set; }
    public required string mediaTagVersion { get; set; }
    public required MovieMetadata[] metadata { get; set; }
}

public class MovieResponse
{
    public required Movie mediaContainer { get; set; }
}
