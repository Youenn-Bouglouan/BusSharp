module HttpUtils

    open System.Text
    open System.IO
    open System.Net
    open System

    let SendPostRequestWithFormUrlEncodedParams (url:string) (content:string) =
        // Prepare request
        let req = HttpWebRequest.Create(url) :?> HttpWebRequest
        req.ProtocolVersion <- HttpVersion.Version10
        req.Method <- "POST"
        req.ContentType <- "application/x-www-form-urlencoded"
        let reqcontent = Encoding.ASCII.GetBytes(content)
        req.ContentLength <- int64 content.Length
        // Send request
        let reqStream = req.GetRequestStream()
        reqStream.Write(reqcontent, 0, content.Length)
        reqStream.Close()
        // process response
        let resp = req.GetResponse()
        let respStream = resp.GetResponseStream()
        let reader = new StreamReader(respStream)
        // return response content
        reader.ReadToEnd()