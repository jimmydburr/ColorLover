# ColorLover

This project sends a random color palette from [colourlovers.com](http://www.colourlovers.com/palettes) to a slack web-hook URL. At my employer we have a channel setup specifically for our graphic design department called colorlovers which is fed by this code. It is for color palette inspiration. Below is a screenshot.

![Slack channel for Creatives](http://concoctedlogic.com/wp-content/uploads/2015/12/colorlovers-channel.png)

The code is written in F# and runs on Mac, Linux, and Windows. Essentially, the following summarizes what the code does.

* uses the awesome [F# JSON type provider](http://fsharp.github.io/FSharp.Data/) to access the colour lover api found [here] (http://www.colourlovers.com/api/)
* extracts, transforms, converts and stores the colors from the api
* creates a graphical (400px x 50px) bitmap image of the pallette
* generates the JSON object for slack
* posts to the slack web-hook

The resulting .exe is a console application and can be run directly from the command line.

    > ColorLover.exe // on windows
    $ mono ColorLover.exe  // on unix

## Build Requirements

The only package required is FSharp.Data. The package should install automatically when you 'restore packages'.

## Maintainer

- [@jimmydburr](https://github.com/jimmydburr)

This is the first thing I've written with F# and my first foray into functional programming. I'm sure the code could be made more succinct and improved. As I am learning, I would appreciate any comments or instruction you would like to offer. Simply open an issue on this repo.
