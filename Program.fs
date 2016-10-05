namespace BusSharp

module Program =

    open System
    open GpsData

    [<EntryPoint>]
    let main argv = 

        // where to save the file
        let filename = "gpsData_"
        let savePath = @"c:\DATA\DEV\FSharp\MyApp\SavedData\"
        let timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
        let fullFilename = sprintf "%s%s.json" filename timestamp
        let fullFilenameWithPath = sprintf "%s%s" savePath fullFilename

        let lines= [
            {Name="a"; Type= Bus};
            {Name="17"; Type= Tramway}
        ]
        
        // Save a json file with the gps data for the lines above
        GpsData.saveGpsDataAsync fullFilenameWithPath lines |> Async.RunSynchronously

        0 // exit code