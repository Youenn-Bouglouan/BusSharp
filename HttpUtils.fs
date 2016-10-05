namespace BusSharp

module HttpUtils =

    open System.IO
    open System.Text
    open System.Net

    // Create and send a HTTP:POST request with www-form-urlencoded parameters
    let PostUrlEncodedAsync (url:string) (content:string) = async {

        // Prepare request
        let req = HttpWebRequest.Create(url) :?> HttpWebRequest
        req.Method <- "POST"
        req.ContentType <- "application/x-www-form-urlencoded"
        let reqcontent = Encoding.ASCII.GetBytes(content)

        // Send request
        use! reqStream = req.GetRequestStreamAsync() |> Async.AwaitTask
        reqStream.Write(reqcontent, 0, content.Length)
        reqStream.WriteAsync(reqcontent, 0, content.Length) |> ignore

        // process response
        let! resp = req.GetResponseAsync() |> Async.AwaitTask
        let respStream = resp.GetResponseStream()
        let reader = new StreamReader(respStream)

        // return response content
        let! result = reader.ReadToEndAsync() |> Async.AwaitTask
        return result
    }