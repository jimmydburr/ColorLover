open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open System
open System.Drawing

type Palette = JsonProvider<"http://www.colourlovers.com/api/palette/92095?format=json&showPaletteWidths=1">
let apiUrlForPalettes = "http://www.colourlovers.com/api/palettes/random?format=json&showPaletteWidths=1"

let myPalette = Palette.Load(apiUrlForPalettes)
printfn "%A\n\n" myPalette
let paletteColors = myPalette.[0].Colors  |> Array.map (fun x -> "#" + x) |> Array.map ColorTranslator.FromHtml
let colorBrushes = paletteColors |> Array.map (fun x -> new SolidBrush(x))
let colorWidths = myPalette.[0].ColorWidths |> Array.map (fun x -> 400.0M * x)
printfn "%A\n\n" colorWidths
let r = System.Console.ReadKey()
exit 0

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
