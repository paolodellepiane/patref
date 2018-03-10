namespace Patref.Controllers
open Microsoft.AspNetCore.Mvc
open U

type SpaceController () =
    inherit Controller()
    static let mutable space = Set.empty;

    [<HttpGet("space")>]
    member this.Get() = space;

    [<HttpDelete("space")>]
    member this.Delete() = space <- Set.empty; space

    [<HttpPost("point")>]
    member this.Post([<FromBody>]points:Point list) =
        space <- space + (points |> Set.ofList)
        space
        
    [<HttpGet("lines/{n}")>]
    member this.Get(n:int) = U.getCollinearLines space n


