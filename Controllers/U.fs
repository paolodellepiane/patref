module U
type Point = { x:float; y:float }
let origin a b = (b.x * a.y - a.x * b.y) / (b.x - a.x)
let slope a b = (b.y - a.y) / (b.x - a.x)
type Line = { m:float; q:float } 
type Line with static member Create a b = { m=origin a b; q=slope a b }
type Space = Point Set

let withPoints (lines:Map<Line, Space>) (points:Point*Point) =
    match points with
    | a, b when a = b -> lines
    | a, b ->
        let line = Line.Create a b
        match Map.tryFind line lines with
        | Some s -> Map.add line (s.Add(a).Add(b)) lines
        | None -> Map.add line Set.empty lines

let permute (arr:'T[]) =
    [ for i in [0..arr.Length - 1] do
        for j in [1..arr.Length - 1] do
            yield (arr.[i], arr.[j]) ]

let calculateLines (space:Space) =
    space
    |> Array.ofSeq
    |> permute
    |> Seq.fold withPoints Map.empty

let getCollinearLines space n =
    calculateLines(space)
    |> Map.toArray
    |> Array.map (fun (_, v) -> v)
    |> Array.filter (fun v -> v.Count >= n)
