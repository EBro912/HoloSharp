![enter image description here](https://i.imgur.com/ewAHUPI.png) 
# HoloSharp
[![NuGet](https://img.shields.io/nuget/v/HoloSharp.svg)](https://www.nuget.org/packages/HoloSharp)

HoloSharp is a C# Wrapper for the [HoloTools](https://hololive.jetri.co/#/) API. It also happens to be my very first API Wrapper!
It is built for the .NET Framework.

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

## Installation
HoloSharp can be installed via NuGet:

```PM> Install-Package HoloSharp```

## Getting Started
```csharp
 HoloSharp holoSharp = new HoloSharp();
 VTuber v = holoSharp.GetChannelByName("Pekora");
 Console.WriteLine(v.Name); // Pekora Ch. 兎田ぺこら
 Console.WriteLine(v.ChannelId); // UC1DCedRgGHBdm81E1llLhOQ
 // ...
```