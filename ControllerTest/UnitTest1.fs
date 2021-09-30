module ControllerTest

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<Test>]
let ``When 2 is added to 2 expect 4``() =
    Assert.AreEqual(4, 2+2)