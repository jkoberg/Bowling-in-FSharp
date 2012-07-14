module bowling_tests

open bowling
open NUnit.Framework

[<TestFixture>]
type BowlingTests() =

    let repeat items =
      seq { while true do yield! items }

    let makeListOf count items =
      repeat items |> Seq.take count |> List.ofSeq

    type TestResult = Success | Failure

    member self.do_test(expected_score, throws) =
      let result = bowling_game throws
      printfn "%A" result
      let outcome =
        match result with
          | FinishedGame(expected_score, _) -> Success
          | _ -> Failure
      assert (outcome = Success)

    [<Test>]
    member self.test_dutch() =
      self.do_test(200, makeListOf 17 [Drop 5; Drop 5; Drop 10])

    [<Test>]
    member self.test_perfect() =
      self.do_test(300, makeListOf 12 [Drop 10])
    
    [<Test>]
    member self.test_normal() =
      self.do_test(60, makeListOf 20 [Drop 3])
    
    [<Test>]
    member self.test_short() =
      self.do_test(100, makeListOf 5 [Drop 3])
    
    [<Test>]
    member self.test_gutter() =
      self.test(0, makeListOf 20 [Drop 0])

