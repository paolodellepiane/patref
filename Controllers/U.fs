module U

type Point = { x:float; y:float }
type Space = Point Set
let origin a b = (b.x * a.y - a.x * b.y) / (b.x - a.x)

let slope a b = (b.y - a.y) / (b.x - a.x)

let simpleHash a b =
    if b.x = a.x then a.x.ToString()
    else sprintf "%f,%f" (slope a b) (origin a b)

let withPoints (lines:Map<string, Space>) (points:Point*Point) =
    match points with
    | a, b when a = b -> lines
    | a, b ->
        let hash = simpleHash a b
        match Map.tryFind hash lines with
        | Some s -> Map.add hash (s.Add(a).Add(b)) lines
        | None -> Map.add hash Set.empty lines

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
