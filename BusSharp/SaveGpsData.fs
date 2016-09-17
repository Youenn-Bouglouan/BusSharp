module SaveGpsData =

    open HttpUtils
    open System
    open System.IO

    type TransportType =
        | Bus
        | Tramway

    type Line = {Name: string; Type: TransportType}

    let getRequestParam line =
        match line.Type with
        | Bus -> sprintf "busList[bus][]=%s" line.Name    
        | Tramway -> sprintf "busList[tram][]=%s" line.Name

    let getRequestParams lines =
        lines
        |> List.fold (fun (str:string) cur -> 
        if str.Length = 0 
            then (getRequestParam cur) 
            else (str + "&" + getRequestParam cur)) ""


    let mpkWroclawGpsDataUrl = "http://mpk.wroc.pl/position.php"
    let linesRequestParams = getRequestParams [{Name="a"; Type= Bus};{Name="17"; Type= Tramway}]
    let gpsData = SendPostRequestWithFormUrlEncodedParams mpkWroclawGpsDataUrl linesRequestParams

    let filename = "gpsData_"
    let savePath = @"c:\DATA\DEV\FSharp\MyApp\SavedData\"
    let timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
    let fullFilename = sprintf "%s%s.json" filename timestamp
    let fullFilenameWithPath = sprintf "%s%s" savePath fullFilename

    let SaveData (responseContent:string) (filename:string) =
        use writer = new StreamWriter(filename)
        writer.Write(responseContent)
        printfn "File '%s' saved" filename

    let saveData = SaveData gpsData fullFilenameWithPath