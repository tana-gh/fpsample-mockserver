# fpsample-mockserver

## Mock server for fpsample projects

This mock server is used for the other projects of fpsample.
This server listens on port `8080`, and on any client's `GET` requests at address `/`, sends following JSON text.

```
{
    "name": "John Smith",
    "age": 20
}
```

## How to use

Execute following command, to run the mock server.

```
$ dotnet run
```
