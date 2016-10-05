namespace BusSharp

module Program =

    open System
    open GpsData
    open System.IO

    // where to save the file
    let savePath = "./SavedData/"
    let filename = "gpsData_"

    let getFilename () =
        let timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
        let fullFilename = sprintf "%s%s.json" filename timestamp
        sprintf "%s%s" savePath fullFilename
        
    [<EntryPoint>]
    let main argv =         
        // Create the folder if it doesn't exist yet
        if not <| Directory.Exists(savePath) then
            printfn "Creating folder '%s'..." savePath
            Directory.CreateDirectory(savePath) |> ignore
            printfn "Folder '%s' created." savePath

        // Retrieve gps data for the following lines
        let lines= [
            {Name="a"; Type= Bus};
            {Name="17"; Type= Tramway}
        ]
        
        // Save a json file with the gps data for the lines above
        DateTime.Now.ToString() |> printfn "before1: %s"
        GpsData.saveGpsDataAsync (getFilename()) lines |> Async.RunSynchronously
        DateTime.Now.ToString() |> printfn "after1: %s"
        Threading.Thread.Sleep(5000)
        DateTime.Now.ToString() |> printfn "before2: %s"
        GpsData.saveGpsDataAsync (getFilename()) lines |> Async.RunSynchronously
        DateTime.Now.ToString() |> printfn "after2: %s"

        0 // exit code