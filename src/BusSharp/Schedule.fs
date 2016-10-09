module Schedule

    open System.Net
    open System
    open Ionic.Zip

    type TransportType =
        | Bus
        | Tramway

    type Line = {Name: string; Type: TransportType}

    let ScheduleLink = "http://www.wroclaw.pl/open-data/opendata/rozklady/OtwartyWroclaw_rozklad_jazdy_XML.zip"

    let webClient = new WebClient()
    //let zipStream = webClient.OpenRead(ScheduleLink)

    webClient.DownloadFile(ScheduleLink, @"C:\Test\Schedule.zip")
    //let zipFile = ZipFile.Read(zipStream)
    let zipFile = ZipFile.Read(@"C:\Test\Schedule.zip")
    let extractedFile = zipFile.ExtractAll(@"C:\Test", ExtractExistingFileAction.OverwriteSilently)

    //let unzipFile(zipFolder: string, unzipFolder: string) =
    //    use zip1 = ZipFile.Read(zipFolder)
    //    for e in zip1 do
    //        e.Extract(unzipFolder, ExtractExistingFileAction.OverwriteSilently)
    //
    //// download the contents of a web page
    //let downloadUriToFile url targetfile =        
    //    let request = WebRequest.Create(Uri(url)) 
    //    use response = request.GetResponse() 
    //    use stream = response.GetResponseStream() 
    //    use reader = new IO.StreamReader(stream) 
    //    let timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm")
    //    let path = sprintf "%s.%s.html" targetfile timestamp 
    //    use writer = new IO.StreamWriter(path) 
    //    writer.Write(reader.ReadToEnd())
    //    printfn "finished downloading %s to %s" url path

