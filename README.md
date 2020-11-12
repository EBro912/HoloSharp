![enter image description here](https://i.imgur.com/ewAHUPI.png) 
# HoloSharp
[![NuGet](https://img.shields.io/nuget/v/HoloSharp.svg)](https://www.nuget.org/packages/HoloSharp)

HoloSharp is a C# Wrapper for the [HoloTools](https://hololive.jetri.co/#/) API. It also happens to be my very first API Wrapper!
It is built using .NET Standard 2.0

## Dependancies
The only dependancy is Newtonsoft.Json, version 12.0.2 or greater.


## Features

 - Retrieve data for all VTubers supported by HoloTools
 - Retrieve YouTube channels by name and by ID (bilibili currently not supported)
 - Retrieve Live, Upcoming, and Ended Livestreams
 - Retrieve regular videos uploaded on VTuber's channels
 - Retrieve videos using a title query and video ID
 - Retrieve comments using a query
 - All requests are handled by the wrapper, and uses its own HttpClient
 - Easy to setup and use!
 
 To Do:
 - In-Depth error handling, especially errors returned by the API

## Installation
HoloSharp can be installed via NuGet:

```PM> Install-Package HoloSharp```

## Getting Started
```csharp
 HoloClient holoClient = new HoloClient();
 VTuber v = holoClient.GetChannelByName("Pekora");
 Console.WriteLine(v.Name); // Pekora Ch. 兎田ぺこら
 Console.WriteLine(v.ChannelId); // UC1DCedRgGHBdm81E1llLhOQ
 // ...
```

## Documentation
Further help and documentation alongside many examples can be found [here.](https://ebro912.gitbook.io/holosharp/)
Or, you can join the Hololive Creators Club Discord, and ask @All Toasters Toast Toast #0001 for help. (That's me!)
The invite to the server can be found [here.](https://discord.gg/xJd9Der)
