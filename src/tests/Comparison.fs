[<NUnit.Framework.TestFixture>] 
module Fable.Tests.Comparison
open NUnit.Framework
open Fable.Tests.Util
open System.Collections.Generic

[<Test>]
let ``Array equality works``() =  
    let xs1 = [| 1; 2; 3 |]
    let xs2 = [| 1; 2; 3 |]
    let xs3 = [| 1; 2; 4 |]
    equal true (xs1 = xs2)
    equal false (xs1 = xs3)
    equal true (xs1 <> xs3)
    equal false (xs1 <> xs2)

[<Test>]
let ``Tuple equality works``() =  
    let xs1 = ( 1, 2, 3 )
    let xs2 = ( 1, 2, 3 )
    let xs3 = ( 1, 2, 4 )
    equal true (xs1 = xs2)
    equal false (xs1 = xs3)
    equal true (xs1 <> xs3)
    equal false (xs1 <> xs2)

[<Test>]
let ``List equality works``() =  
    let xs1 = [ 1; 2; 3 ]
    let xs2 = [ 1; 2; 3 ]
    let xs3 = [ 1; 2; 4 ]
    equal true (xs1 = xs2)
    equal false (xs1 = xs3)
    equal true (xs1 <> xs3)
    equal false (xs1 <> xs2)

[<Test>]
let ``Set equality works``() =  
    let xs1 = Set [ 1; 2; 3 ]
    let xs2 = Set [ 1; 2; 3 ]
    let xs3 = Set [ 1; 2; 4 ]
    let xs4 = Set [ 3; 2; 1 ]
    let xs5 = Set [ 1; 2; 3; 1 ]
    equal true (xs1 = xs2)
    equal false (xs1 = xs3)
    equal true (xs1 <> xs3)
    equal false (xs1 <> xs2)
    equal true (xs1 = xs4)
    equal false (xs1 <> xs5)
    
[<Test>]
let ``Map equality works``() =  
    let xs1 = Map [ ("a", 1); ("b", 2); ("c", 3) ]
    let xs2 = Map [ ("a", 1); ("b", 2); ("c", 3) ]
    let xs3 = Map [ ("a", 1); ("b", 2); ("c", 4) ]
    let xs4 = Map [ ("c", 3); ("b", 2); ("a", 1) ]
    equal true (xs1 = xs2)
    equal false (xs1 = xs3)
    equal true (xs1 <> xs3)
    equal false (xs1 <> xs2)
    equal true (xs1 = xs4)
    
type UTest = A of int | B of int

[<Test>]
let ``Union equality works``() =  
    let u1 = A 2
    let u2 = A 2
    let u3 = A 4
    let u4 = B 2
    equal true (u1 = u2)
    equal false (u1 = u3)
    equal true (u1 <> u3)
    equal false (u1 <> u2)
    equal false (u1 = u4)

type RTest = { a: int; b: int }
    
[<Test>]
let ``Record equality works``() =  
    let r1 = { a = 1; b = 2 }
    let r2 = { a = 1; b = 2 }
    let r3 = { a = 1; b = 4 }
    equal true (r1 = r2)
    equal false (r1 = r3)
    equal true (r1 <> r3)
    equal false (r1 <> r2)
    
type Test(i: int) =
    member x.Value = i
    override x.GetHashCode() = i.GetHashCode()
    override x.Equals(another) =
        match another with
        | :? Test as another -> another.Value + 1 = x.Value
        | _ -> false
    interface System.IComparable with
        member x.CompareTo(another) =
            match another with
            | :? Test as another ->compare (another.Value + 1) x.Value
            | _ -> -1
            
[<Test>]
let ``Equality with objects implementing IComparable works``() =  
    let c1 = Test(5)
    let c2 = Test(4)
    let c3 = Test(5)
    equal true (c1 = c2)
    equal false (c1 = c3)
    equal true (c1 <> c3)
    equal false (c1 <> c2)

[<Test>]
let ``Array comparison works``() =  
    let xs1 = [| 1; 2; 3 |]
    let xs2 = [| 1; 2; 3 |]
    let xs3 = [| 1; 2; 4 |]
    let xs4 = [| 1; 2; 2 |]
    let xs5 = [| 1; 2 |]
    let xs6 = [| 1; 2; 3; 1 |]
    equal 0 (compare xs1 xs2)
    equal -1 (compare xs1 xs3)
    equal true (xs1 < xs3)
    equal 1 (compare xs1 xs4)
    equal false (xs1 < xs4)
    equal 1 (compare xs1 xs5)
    equal true (xs1 > xs5)
    equal -1 (compare xs1 xs6)
    equal false (xs1 > xs6)

[<Test>]
let ``Tuple comparison works``() =  
    let xs1 = ( 1, 2, 3 )
    let xs2 = ( 1, 2, 3 )
    let xs3 = ( 1, 2, 4 )
    let xs4 = ( 1, 2, 2 )
    equal 0 (compare xs1 xs2)
    equal -1 (compare xs1 xs3)
    equal true (xs1 < xs3)
    equal 1 (compare xs1 xs4)
    equal false (xs1 < xs4)

[<Test>]
let ``List comparison works``() =  
    let xs1 = [ 1; 2; 3 ]
    let xs2 = [ 1; 2; 3 ]
    let xs3 = [ 1; 2; 4 ]
    let xs4 = [ 1; 2; 2 ]
    let xs5 = [ 1; 2 ]
    let xs6 = [ 1; 2; 3; 1 ]
    equal 0 (compare xs1 xs2)
    equal -1 (compare xs1 xs3)
    equal true (xs1 < xs3)
    equal 1 (compare xs1 xs4)
    equal false (xs1 < xs4)
    equal 1 (compare xs1 xs5)
    equal true (xs1 > xs5)
    equal -1 (compare xs1 xs6)
    equal false (xs1 > xs6)

[<Test>]
let ``Set comparison works``() =  
    let xs1 = Set [ 1; 2; 3 ]
    let xs2 = Set [ 1; 2; 3 ]
    let xs3 = Set [ 1; 2; 4 ]
    let xs4 = Set [ 1; 2; 2 ]
    let xs5 = Set [ 1; 2 ]
    let xs6 = Set [ 1; 2; 3; 1 ]
    equal 0 (compare xs1 xs2)
    equal -1 (compare xs1 xs3)
    equal true (xs1 < xs3)
    equal 1 (compare xs1 xs4)
    equal false (xs1 < xs4)
    equal 1 (compare xs1 xs5)
    equal true (xs1 > xs5)
    equal 0 (compare xs1 xs6)
    
[<Test>]
let ``Map comparison works``() =  
    let xs1 = Map [ ("a", 1); ("b", 2); ("c", 3) ]
    let xs2 = Map [ ("a", 1); ("b", 2); ("c", 3) ]
    let xs3 = Map [ ("a", 1); ("b", 2); ("c", 4) ]
    let xs4 = Map [ ("a", 1); ("b", 2); ("c", 2) ]
    let xs5 = Map [ ("a", 1); ("b", 2) ]
    let xs6 = Map [ ("a", 1); ("b", 2); ("c", 3); ("d", 1) ]
    equal 0 (compare xs1 xs2)
    equal -1 (compare xs1 xs3)
    equal true (xs1 < xs3)
    equal 1 (compare xs1 xs4)
    equal false (xs1 < xs4)
    equal 1 (compare xs1 xs5)
    equal true (xs1 > xs5)
    equal -1 (compare xs1 xs6)
    equal false (xs1 > xs6)

[<Test>]
let ``Union comparison works``() =  
    let u1 = A 2
    let u2 = A 2
    let u3 = A 4
    let u4 = A 1
    let u5 = B 2
    equal 0 (compare u1 u2)
    equal -1 (compare u1 u3)
    equal true (u1 < u3)
    equal 1 (compare u1 u4)
    equal false (u1 < u4)
    (compare u1 u5) = 0 |> equal false
    
[<Test>]
let ``Record comparison works``() =  
    let r1 = { a = 1; b = 2 }
    let r2 = { a = 1; b = 2 }
    let r3 = { a = 1; b = 4 }
    equal 0 (compare r1 r2)
    (compare r1 r3) = 0 |> equal false
    
[<Test>]
let ``Comparison with objects implementing IComparable works``() =  
    let c1 = Test(5)
    let c2 = Test(4)
    let c3 = Test(5)
    equal 0 (compare c1 c2)
    equal 1 (compare c1 c3)
    equal true (c1 > c3)    