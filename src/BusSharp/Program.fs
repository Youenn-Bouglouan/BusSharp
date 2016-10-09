namespace BusSharp

module Program =

    open System
    open GpsData
    open FileUtils
    open System.IO

    // Settings for GpsData module
    let savePath = "./SavedData/"
    let filename = "gpsData_"

    let getFilename () = 
        FileUtils.getFilename filename "json" true
        |> sprintf "%s%s" savePath

    [<EntryPoint>]
    let main argv =
        
        printfn "[%s] starting BusSharp..." (DateTime.Now.ToString("yyyy-MM dd_HH-mm-ss"))
        
        let exitCommand = 'q'
        let exitInstruction = sprintf "Press '%s' to exit" (exitCommand.ToString())       
        printfn "%s" exitInstruction
        
        FileUtils.createFolder savePath

        // Retrieve gps data for the following lines
        let lines= [
            {Name="a"; Type= Bus};
            {Name="17"; Type= Tramway}
        ]

        // Save GPS data every 30s
        GpsData.saveEvery 30 getFilename lines |> Async.StartImmediate

        // Do not exit until the 'q' key is pressed
        let mutable exitApp = false
        while not exitApp do
            match System.Console.ReadKey().KeyChar with
            | x when x = exitCommand -> 
                printfn "\n[%s] exiting BusSharp..." (DateTime.Now.ToString("yyyy-MM dd_HH-mm-ss"))
                exitApp <- true
            | (invalidValue) -> printfn "\nInvalid command '%s'. %s" (invalidValue.ToString()) exitInstruction 

        0 // exit code