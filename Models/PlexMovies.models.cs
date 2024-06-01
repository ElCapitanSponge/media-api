namespace media_api.Models;

// INFO: Movies
public class MoviesBase : MediaCommon
{
    public required TagItem[] genre { get; set; }
    public required TagItem[] country { get; set; }
    public required TagItem[] director { get; set; }
    public required TagItem[] writer { get; set; }
    public required TagItem[] role { get; set; }
}

public class Movie
{
	// TODO: Add the allocations for a movies items
}

public class MovieResponse
{
    public required Movie mediaContainer { get; set; }
}
