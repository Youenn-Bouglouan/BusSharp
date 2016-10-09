open System

let runEvery interval job =
    let rec loop time work =
        Threading.Thread.Sleep(time:int)
        work()
        loop time work

    loop interval job

let printSomething () =
    printfn "test" |> ignore

runEvery 5 printSomething