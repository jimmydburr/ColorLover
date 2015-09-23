open FSharp.Data
open FSharp.Data.HttpRequestHeaders

//type Palette = JsonProvider<"http://www.colourlovers.com/api/palettes/random?format=json">
type Palette = JsonProvider<"http://www.colourlovers.com/api/palette/92095?format=json">
//let apiUrlForPalettes = "http://www.colourlovers.com/api/palette/92095?format=json"
let apiUrlForPalettes = "http://www.colourlovers.com/api/palettes/random?format=json"

let myPalette = Palette.Load(apiUrlForPalettes)
//printfn "%A\n%A\n%A\n\n" myPalette.[0].BadgeUrl myPalette.[0].Url myPalette.[0].Title
printfn "%A\n\n" myPalette.[0]

let myTextRequest = "{
    \"username\" : \"" + myPalette.[0].Title + "\",
    \"icon_url\" : \"" + myPalette.[0].ImageUrl + "\",
    \"text\" : \"<" + myPalette.[0].Url + ">\"
}"
printfn "%s\n" myTextRequest
// hightower slack #creative channel
Http.RequestString(
    "https://hooks.slack.com/services/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",
    headers = [ ContentType HttpContentTypes.Json ],
    body = TextRequest myTextRequest
) |> printfn "%A"
    
let pause = System.Console.ReadKey()