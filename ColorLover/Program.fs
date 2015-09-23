open FSharp.Data
open FSharp.Data.HttpRequestHeaders

let apiUrlForPalettes = "http://www.colourlovers.com/api/palettes?format=json"
type Palette = JsonProvider<"http://www.colourlovers.com/api/palette/92095?format=json">

let myPalette = Palette.Load(apiUrlForPalettes)
//printfn "%A\n%A\n%A\n\n" myPalette.[0].BadgeUrl myPalette.[0].Url myPalette.[0].Title
printfn "%A\n\n" myPalette.[0]
let myTextRequest = "{ \"URL\" : \"<" + myPalette.[0].Url + ">\" }"
printfn "%s" myTextRequest

Http.RequestString(
    "http://httpbin.org/post", 
    headers = [ ContentType HttpContentTypes.Json ],
    body = TextRequest myTextRequest
) |> printfn "%A"
    
let pause = System.Console.ReadKey()