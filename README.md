# Avola-client-aspnetcore
This is a production example of the Avola client built with a standard .NET core Web api setup. 

## Before running 
Include a appsettings.json in the solution with the following:

```
{
  "AppSettings": {
    "AuthUrl": "https://login.avo.la/connect/token",
    "AuthScope": "avola-api-client",
    "BaseUri": "https://example.api.avo.la/api/",
    "Client": "yourclientid",
    "Secret": "yourclientsecret"
  }
}
```