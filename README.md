# Media Api

An api for managing a Plex library.

It allows the addition of movies (Radarr), shows (Sonarr) and display of the
Plex libraries.

Searching of content is facilitated vai integration with IMDB.

## Configuration

### .env

```env
PLEX_TOKEN="<Plex server token>"
PLEX_ADDRESS="<The address of the plex server>"
PLEX_PORT=<The port for plex communication (default is 32400)>
```

## Development

Running the api in development mode with hot reloading:

```SHELL
dotnet watch
```

Running the hot reload using HTTPS:

```SHELL
dotnet watch --launch-profile https
```

## Deploying to Local Docker

```SHELL
docker compose up
```

## TODO

The following tasks are still required:

- Connection and handling for Sonarr
- Connection and handling for Radarr
- Connection and handling for Readarr?
