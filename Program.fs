namespace BusSharp

module Program =

    open System
    open GpsData
    open System.IO

    [<EntryPoint>]
    let main argv = 

        // where to save the file

        let getFilename =
            let filename = "gpsData_"
            let savePath = "./SavedData/"
            
            if not <| Directory.Exists(savePath) then
                printfn "Creating folder '%s'..." savePath
                Directory.CreateDirectory(savePath) |> ignore
                printfn "Folder '%s' created." filename

            let timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
            let fullFilename = sprintf "%s%s.json" filename timestamp
            sprintf "%s%s" savePath fullFilename

        let lines= [
            {Name="a"; Type= Bus};
            {Name="17"; Type= Tramway}
        ]
        
        // Save a json file with the gps data for the lines above
        GpsData.saveGpsDataAsync getFilename lines |> Async.RunSynchronously

        0 // exit code