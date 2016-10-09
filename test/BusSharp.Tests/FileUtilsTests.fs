namespace BusSharp.Tests

module FileUtilsTests =

    open Xunit
    open System
    open BusSharp.FileUtils

    [<Fact>]
    let ``FileUtils.getFilename handles extensions with a dot``() =
        let actual = getFilename "test_" ".json" false
        let expected = "test_.json"
        Assert.Equal(actual, expected)

    [<Fact>]
    let ``FileUtils.getFilename handles extensions without a dot``() =
        let actual = getFilename "test_" "json" false
        let expected = "test_.json"
        Assert.Equal(actual, expected)

        
        
