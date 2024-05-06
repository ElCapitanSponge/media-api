namespace media_api.Models;

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
public class LibraryWrapper : CoreWrapper
{
	/* public LibraryRecord<MovieBase, ShowBase>[]? Metadata { get; set; } */
	public object[]? Metadata { get; set; }
}

public class LibraryResponse
{
	public required string art { get; set; }
	public required string content { get; set; }
	public required int librarySectionID { get; set; }
	public required string librarySectionTitle { get; set; }
	public required string librarySectionUUID { get; set; }
	public required string thumb { get; set; }
	public required string viewGroup { get; set; }
	public required LibraryWrapper MediaContainer { get; set; }
}

public abstract class LibraryRecord<M, S>
{
	public abstract T Match<T>(Func<M, T> m, Func<S, T> s);

	private LibraryRecord() { }

	public sealed class CaseMovie : LibraryRecord<M, S>
	{
		public readonly M Item;
		public CaseMovie(M item) : base()
		{
			this.Item = item;
		}
		public override T Match<T>(Func<M, T> m, Func<S, T> s)
		{
			return m(Item);
		}
	}

	public sealed class CaseShow : LibraryRecord<M, S>
	{
		public readonly S Item;
		public CaseShow(S item) : base()
		{
			this.Item = item;
		}
		public override T Match<T>(Func<M, T> m, Func<S, T> s)
		{
			return s(Item);
		}
	}
}

// INFO: Movies
public class MovieBase
{
}

// INFO: Shows
public class ShowBase
{

}
