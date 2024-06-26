using media_api.Models.Plex.Core;

namespace media_api.Models.Plex.Accounts;

// INFO: Accounts
public class AccountsWrapper : CoreWrapper
{
    public required Account[] Account { get; set; }
}

public class AccountsResponse
{
    public required AccountsWrapper MediaContainer { get; set; }
}

public class Account
{
    public required int id { get; set; }
    public required string key { get; set; }
    public required string name { get; set; }
    public required string defaultAudioLanguage { get; set; }
    public required bool autoSelectAudio { get; set; }
    public required string defaultSubtitleLanguage { get; set; }
    public required int subtitleMode { get; set; }
    public required string thumb { get; set; }
}
