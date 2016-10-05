namespace BusSharp

module GpsData =

    open HttpUtils
    open System.IO

    type TransportType =
        | Bus
        | Tramway

    type Line = {Name: string; Type: TransportType}

    let mpkWroclawGpsDataUrl = "http://mpk.wroc.pl/position.php"

    // Create a www-form-urlencoded string from a 'Line'
    let private formatLine line =
        match line.Type with
        | Bus -> sprintf "busList[bus][]=%s" line.Name    
        | Tramway -> sprintf "busList[tram][]=%s" line.Name

    // Create a www-form-urlencoded string from a list of 'Line'
    let private formatLines lines =
        lines
        |> List.fold (fun (str:string) cur -> 
        if str.Length = 0 
            then (formatLine cur) 
            else (str + "&" + formatLine cur)) ""

    // Retrieve gps data from the MPK server
    let private getGpsDataAsync = PostUrlEncodedAsync mpkWroclawGpsDataUrl
    
    // Retrieve and save gps data to a file
    let saveGpsDataAsync (filename:string) lines = async {

        use writer = new StreamWriter(new FileStream(filename, FileMode.CreateNew))
        let! gpsData = formatLines lines |> getGpsDataAsync
        writer.WriteAsync(gpsData) |> ignore
        printfn "File '%s' saved" filename
    }