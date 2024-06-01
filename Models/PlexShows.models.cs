namespace media_api.Models;

// INFO: Shows
public class ShowsBase : MediaCommon
{
    public required string theme { get; set; }
    public required int leafCount { get; set; }
    public required int viewedLeafCount { get; set; }
    public required int childCount { get; set; }
}

public class ShowDirectory
{
    public required int leafCount { get; set; }
    public required string thumb { get; set; }
    public required int viewedLeafCount { get; set; }
    public required string key { get; set; }
    public required string title { get; set; }
}

public class ShowMetadata
{
    public required int ratingKey { get; set; }
    public required string key { get; set; }
    public required int parentRatingKey { get; set; }
    public required string guid { get; set; }
    public required string parentSlug { get; set; }
    public required string parentStudio { get; set; }
    public required string type { get; set; }
    public required string title { get; set; }
    public required string parentKey { get; set; }
    public required string parentTitle { get; set; }
    public required string summary { get; set; }
    public required int index { get; set; }
    public required int parentIndex { get; set; }
    public required int parentYear { get; set; }
    public required string thumb { get; set; }
    public required string art { get; set; }
    public required string parentThumb { get; set; }
    public required string parentTheme { get; set; }
    public required int leafCount { get; set; }
    public required int viewedLeafCount { get; set; }
    public required int addedAt { get; set; }
    public required int updatedAt { get; set; }
}

public class Show
{
    public required int size { get; set; }
    public required bool allowSync { get; set; }
    public required string art { get; set; }
    public required string identifier { get; set; }
    public required int key { get; set; }
    public required int librarySectionId { get; set; }
    public required string librarySectionTitle { get; set; }
    public required string librarySectionUUID { get; set; }
    public required string mediaTagPrefix { get; set; }
    public required int mediaTagVersion { get; set; }
    public required bool nocache { get; set; }
    public required int parentIndex { get; set; }
    public required string parentTitle { get; set; }
    public required int parentYear { get; set; }
    public required string summary { get; set; }
    public required string theme { get; set; }
    public required string thumb { get; set; }
    public required string title1 { get; set; }
    public required string title2 { get; set; }
    public required string viewGroup { get; set; }
    public required ShowDirectory[] directory { get; set; }
    public required ShowMetadata[] metadata { get; set; }
}

public class ShowResponse
{
    public required Show mediaContainer { get; set; }
}
