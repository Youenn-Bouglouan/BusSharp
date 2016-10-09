namespace BusSharp

module FileUtils =

    open System.IO
    open System

    /// Create the 'path' folder if it doesn't exist yet
    let createFolder path =
        if not <| Directory.Exists(path) then
            printfn "Creating folder '%s'..." path
            Directory.CreateDirectory(path) |> ignore
            printfn "Folder '%s' created." path

    /// Create a file name based on a name and an extension
    let getFilename baseName extension withTimestamp =

        let formattedExtension =
            match extension:string with
            | ext when ext.StartsWith(".") -> sprintf "%s" (ext.Substring(1, ext.Length-1))
            | (ext) -> sprintf "%s" ext
        
        match withTimestamp with
        | true ->
            let timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
            sprintf "%s%s.%s" baseName timestamp formattedExtension
        | _ -> sprintf "%s.%s" baseName formattedExtension