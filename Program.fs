open System
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.Threading
let addPoint (ctx:HttpContext) = async {
  return Some ctx
}

let app =
  choose
    [ GET >=> choose
        [ path "/space" >=> OK "Good bye GET"
          path "/lines/%d" >=> OK "Good bye GET" ]
      POST >=> choose
        [ path "/point" >=> addPoint ] 
      DELETE >=> choose
        [ path "/point/%d" >=> OK "" 
          path "/space" >=> OK "Hello GET" ]
    ]

[<EntryPoint>]
let main argv =
    let cts = new CancellationTokenSource()
    let conf = { defaultConfig with bindings = [ HttpBinding.createSimple HTTP "0.0.0.0" 8083 ];
                                cancellationToken = cts.Token }
    let _, server = startWebServerAsync conf app
    Async.Start(server, cts.Token)
    printfn "Make requests now"
    Console.ReadKey true |> ignore
    0
