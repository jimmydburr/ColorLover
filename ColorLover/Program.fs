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
let colorWidths = myPalette.[0].ColorWidths |> Array.map (fun x -> 400.0M * x) |> Array.map int
let colorXStarts : int array = Array.zeroCreate 5
colorXStarts.[0] <- 0
for i in 1..colorWidths.Length - 1 do
    colorXStarts.[i] <- colorXStarts.[i-1] + colorWidths.[i-1]

//printfn "%A\n\n" colorXStarts
let flag = new Bitmap(400, 50);
let flagGraphics = Graphics.FromImage(flag);
let height = 50
let colorY = 0

for i in 0 .. colorXStarts.Length - 1 do
    flagGraphics.FillRectangle(colorBrushes.[i], colorXStarts.[i], colorY, colorWidths.[i], height)

let flagName = "/var/www/concoctedlogic/palettes/flag" + DateTime.Now.ToString("fffffff") + ".png"
printfn "%s\n" flagName
flag.Save(flagName, System.Drawing.Imaging.ImageFormat.Png)

let myTextRequest = "{
    \"channel\" : \"@jim\",
    \"username\" : \"" + myPalette.[0].Title + "\",
    \"icon_url\" : \"" + myPalette.[0].ImageUrl + "\",
    \"text\" : \"<" + myPalette.[0].Url + ">\",
    \"attachments\": [
        {
            \"fallback\": \"The palette attachment.\",
            \"color\": \"#36a64f\",
            \"image_url\": \"http://concoctedlogic.com/palettes/" + flagName + "\"
        }
    ]
}"
printfn "%s\n" myTextRequest

//let r = System.Console.ReadKey()
//exit 0

// hightower slack #creative channel
Http.RequestString(
    "https://hooks.slack.com/services/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",
    headers = [ ContentType HttpContentTypes.Json ],
    body = TextRequest myTextRequest
) |> printfn "%A"
