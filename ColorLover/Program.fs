open FSharp.Data

//type Palette = XmlProvider<"http://www.colourlovers.com/api/palettes">
//let myPalette = Palette.GetSample()
//printfn "%A \n" (myPalette)
//for item in myPalette.Palettes do
//  printfn " - %s (%s)" (item.Id.ToString()) (item.BadgeUrl.ToString())

let apiUrlForPalettes = "http://www.colourlovers.com/api/palettes?format=json"
type Palette = JsonProvider<"http://www.colourlovers.com/api/palette/92095?format=json">

let myPalette = Palette.Load(apiUrlForPalettes)
printfn "%A\n%A\n%A " myPalette.[0].BadgeUrl myPalette.[0].Url myPalette.[0].Title

let pause = System.Console.ReadKey()