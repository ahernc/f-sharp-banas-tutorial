open System

// Code walk-through of the video by Derek Banas: https://www.youtube.com/watch?v=c7eNDJN758U&t=2s
let hello() = 
    printf "Enter your name: " 

    let name = Console.ReadLine()

    printfn "Hello %s" name
    // Other types we will get to later: 
    // %i %f %b %s %A (internal representation of things like tuples), %O (Object)



let pitest() = 
    // with M on the end, you allow us to display the maximum precision
    let pi = 3.1415926535897932384626433832M
  
    // specify precision... here we would output 3.1416
    printfn "PI: %.4f" pi
 
    // Maximum... which is 27 digits
    printfn "PI: %M" pi



let paddingTest() = 
    // padding:
    printfn "%-5s %5s" "first word" "last word"

    // dymanic padding (asterisk... then specify the precision)
    printfn "%*s" 10 "Hi!"



let bindStuff() = 
    // mostly we will work with immutable data... just to show how mutable woks:
    let mutable weight = 175
    weight <- 170 // <- this is hwo to change a mutable value

    printfn "Weight: %i" weight

    // reference cells: 
    let changeMe = ref 10
    // why use these?
    changeMe := 50   // how to change a reference cell... 

    // you need the ! in order to output a reference cell
    printfn "Change: %i" ! changeMe

    printfn "Change: %i" ! changeMe



let functionsExample() = 
    let getSum(x: int, y: int) : int = x + y
    printfn "5 + 7 = %i" (getSum(5,7))

    // recursive function
    let rec factorial x = 
        if x < 1 then 1
        else x * factorial (x - 1)
    
    // the function will call itself 4 times...
    printfn "Factorial 4: %i" (factorial 4)
    // 1st result = 4 * factorial(3)  = 4 * 6 = 24
    // 2nd result = 3 * factorial(2) = 3 * 2 = 6
    // 3rd result = 1 * factorial(1) =  2 * 1 = 2


// anonymous functions, lambas, pipe
let functionsExample2() = 
    let randomList = [1;2;3]

    // Lamdba  / anonymous function
    let randomList2 = List.map(fun x -> x * 2) randomList

    // %A for things like tuples
    printfn "Double List: %A" randomList2

    // Using the pipe operator... notice how the pipe automaticlly means we 
    // are picking up the list that has just been declared, but without the
    // explicit use of a variable:
    [5;6;7;8]
    // filters only the items in the list that match a condition... using anon function
    |> List.filter (fun v -> (v % 2) = 0) 
    // map will take all of the items of the previous statement and then we do something with the vals
    // here we will double the values
    |> List.map (fun x -> x * 2)
    // then because we are working with current tuple, internal representation
    // is implied because of pipe statements... 
    |> printfn "Even doubles: %A" 


    let multNum x = x * 3
    let addNum y = y + 5

    // the >> and << syntax is nifty: it allows you to specify the order in which
    // functions are executed
    let multAdd = multNum >> addNum // multiply then adds
    let addMult = multNum << addNum // adds then multiplies

    printfn "multAdd : %i" (multAdd 10)
    printfn "addMult : %i" (addMult 10)


let mathFunctions() = 
    printfn "5 + 4 = %i" (5 + 4)
    printfn "5 - 4 = %i" (5 - 4)
    printfn "5 * 4 = %i" (5 * 4)
    printfn "5 / 4 = %i" (5 / 4)
    printfn "5 %% 4 = %i" (5 % 4) 
    printfn "5 ** 2 = %.1f" (5.0 ** 2.0) // squared

    let number = 2
    printfn "Type: %A" (number.GetType())
    
    // cast to another type:
    printfn "A Float: %.2f" (float number)
    printfn "An int: %i" (int 3.14)

    printfn "abs 4.5: %i" (abs -1) // always positive


let stringFunctions() =
    let str1 = "This is a random string"
    // verbatim string:
    let str2 = @"I ignore backslashes \ \ / "

    // triple quoted:
    let str3 = """ "I ignore double quotes and backslashes" """

    let str4 = str1 + "  " + str2

    printfn "Length: %i" (String.length str4)

    printfn "%c" str1.[1] // character in position...

    // 0123 characters in thee sentence... a range function
    printfn "First word %s" (str1.[0..3])

    let upperStr = String.collect(fun c -> sprintf "%c, " c) "commas"
    printfn "Commas: %s" upperStr

    // check if the characters meets  condition... e.g. if ucase
    printfn "Any upper: %b" (String.exists(fun c -> Char.IsUpper(c))str1)
    
    printfn "Is every character a digit?:  %b" (String.forall(fun c -> Char.IsDigit(c)) "1234" ) 

    // apply a function to each index in a string:
    let string1 = String.init 30 (fun i -> i.ToString())
    printf "Numbers: %s" string1

    // apply a function to each ITEM in the string...
    String.iter(fun c -> printfn "%c" c) "Print me out!"


let whileLoopStuff() = 
    let magicNum = "7"
    let mutable guess = ""

    while not (magicNum.Equals(guess)) do
        printf "Guess the number (hint... it's 7 :-): "
        guess <- Console.ReadLine()

    printfn "Correct! You guessed the number!"



let forLoopStuff() = 
    for i = 0 to 10 do
        printfn "%i" i

    for i = 10 downto 1 do
        printfn "%i" i

    for i in [1..10] do
        printfn "%i" i

    // pipe syntax:
    [1..10] |> List.iter(printfn "Num: %i")

    // sum a list: uses reduce then the operation in brackets
    let sum = List.reduce(+) [1..10]
    printfn "Sum %i" sum


 
let conditionalStuff() = 
    // make the original code a bit more interestingg with a match block...
    Console.WriteLine("Im going to tell you what school you should go to: ")
    Console.Write("Enter your age: ")
    
    // To parse input as a specified type, use something like this:
    let age = 
        match Console.ReadLine() |> System.Int32.TryParse with
        | true, theResultInALambda -> theResultInALambda
        | false, _ -> -1 // _ is the lambda for all other cases... 
    
    if age < 0 then 
        printfn "Rubbish... enter a proper age!"
    elif age < 5 then
        printf "Go to preschool"
    elif age = 5 then
        printf "To Kindergarden"
    elif (age > 5) && (age <= 18) then
        let grade = age - 5
        printfn "Go to grade %i" grade
    else
        printfn "Go to college!"

    let gpa = 3.9
    let income = 15000

    // or condition: just like c# ||
    // should you get a grant?
    printfn "College Grant: %b" ((gpa >= 3.8) || (income <= 12000))

    // just use the word "not" 
    printfn "Demonstrating the not operator: %b" (not true)

    // match and guard statements can achieve what we just did above:
    // these are sort of like the switch statement in C#
    let grade2: string = // string is return type
        match age with   // this is the value we are checking
        // and each condition is separated by a pipe
        // the _ is the default when all of the other conditions are false
        | age when age < 5 -> "Preschool"
        | 5 -> "Kindergarten"
        | age when ((age > 5) && (age <= 18)) -> (age - 5).ToString()
        | _ -> "College"
        
    printfn "grade2: %s" grade2



let listStuff() = 
    let list1 = [1;2;3;4]

    // to print the list:
    list1 |> List.iter(printfn "Num: %i")

    // internal representation of a list... this just outputs the contents as you would populate then... [1;2;3;4]
    printfn "%A" list1

    // Another way to initialise a List:
    let list2 = 5::6::7::[]
    printfn "%A" list2

    // Or us the range syntax:
    let list3 = [1..5]

    // The range syntax also works with letters:
    let list4 = ['a'..'g']
    printfn "%A" list4

    // creates 5 indexes.... 
    // Then populate using index * 2
    let list5 = List.init 5 (fun i -> i * 2)
    printfn "%A" list5


    // yield: in this example, we will modify each of the index values, times itself:
    let list6 =  [ for a in 1..5 do yield ( a * a ) ]
    printfn "%A" list6

    // throw in an if condition:
    let list7 =  [ for a in 1..20 do if a % 2 = 2 then yield a ]
    printfn "%A" list7

    // yield bang... generates another list 
    let list8 = [ for a in 1..3 do yield! [ a .. a + 2 ]  ]
    printfn "%A" list8
    
    // Some other functions to handle lists:
    printfn "Length of a list: %i" list8.Length
    printfn "Empty?: %b" list8.IsEmpty
    
    // list 4 was characters...
    printfn "Index 2 = %c" (list4.Item(2))
    printfn "Head = %c" (list4.Head)
    printfn "Tail= %A" (list4.Tail)
    
    // Populate list 9 with even numbers from list3...
    let list9 = list3 |> List.filter (fun x -> x % 2 = 0 )

    let list10 = list9 |> List.map (fun x -> (x*x) )

    // sort a list... 
    printfn "Sorted: %A" (List.sort [5;4;3])

    // Sum up the items in a list... here we are passing in a list on the fly, 1+2+3
    // fun sum pass in the element... 0 is the initialization of the sum as zero
    printfn "Sum: %i" (List.fold (fun sum elem -> sum + elem) 0 [1;2;3])




// 
type emotion = 
| joy = 0
| fear = 1
| anger = 2

let enumStuff() = 
    let myFeeling = emotion.joy

    // Check how we are feeling... 
    match myFeeling with
    | joy -> printfn "I am joyful!"
    | fear -> printfn "I am fearful!"
    | anger -> printfn "I am angry!"
    


// An option is when a function may not return a value.. .
let optionStuff() = 
    // division by zero error... 
    let divide x y = 
        match y with
        | 0  -> None // If we just executed Some(x/y) here, it would throw an unhandles exception
        | _ -> Some(x/y) // Some Option... 

    if (divide 5 0).IsSome then
        printfn "5 / 0 = %A" ((divide 5 0).Value)
    elif (divide 5 0).IsNone then
        printfn "Can't divide by zero!"
    else
        printfn "Something happened!"


let tupleStuff() = 
// A tuple is a comma separated list of values of any type
    let avg (w,x,y,z) : float = 
        let sum = w + x + y + z
        sum / 4.0

    printfn "Avg: %f! " (avg(1.0, 2.0, 3.0, 4.0))


    let myData = ("John", 42, 72.65)
    let (name, age, _) = myData

    printfn "Name: %s" name


// customer record type
type customer = 
    {
        Name : string;
        Balance : float
    }

let recordStuff() = 
    let bob = { Name = "Bob Smith"; Balance = 123.99 }
    printfn "%s owes $%.2f" bob.Name bob.Balance


let sequenceStuff() = 
    // generate a sequence
    let seq1 = seq { 1 .. 100 }
    let seq2 = seq { 0 .. 2 .. 50 }
    let seq3 = seq { 50 .. 1 }

    printfn "%A" seq2

    Seq.toList seq2 |> List.iter (printfn "Num: %i")

    // test if a number is prime:
    let isPrime n  = 
        // check multiple values that are passed inside...
        let rec check i = 
            i > n/2 || (n%i <> 0 && check (i+1))
        check 2
    
    let primeSeq = seq { for n in 1..500 do if isPrime n then yield n}


    // This will print the abbreviated version of the sequence... 
    printfn "%A" primeSeq

    Seq.toList primeSeq |> List.iter(printfn "Prime %i" )
    

let mapStuff() = 
    // Maps... start by generating an empty Map, then add key values 
    let customers = 
        Map.empty
            .Add("Bob Smith", 123.54)
            .Add("Sally Jones", 69.99)

    printfn "Number of customers: %i" customers.Count
    
    let bob = customers.TryFind "Bob Smith" 
    match bob with
    | Some x -> printfn "Balance: %.2f" x
    | None -> printfn "No bob found!"

    if customers.ContainsKey "Bob Smith" then
        printfn "Bob Smith was found!"

    printfn "Bob's balance: %.2f" customers.["Bob Smith"]

    // Remove a customer... the result of the list is assigned to customers2
    let customers2 = Map.remove "Sally Jones" customers

    printfn "new number of customers %i" customers2.Count
        


// Generic stuff...
// Single quote.... generic type. see http://stackoverflow.com/questions/17912115/explain-the-notation-in-f
let addStuff<'T> x y = 
    printfn "%A" (x + y)

let genericStuff() = 
    addStuff<float> 5.5 2.4




let expStuff() = 
    let divide x y = 
        try
            // If we did not have the if statement here, the "Inifinity" error would just appear if we divide by zero.
            if y = 0.0 then raise (System.DivideByZeroException "Can't divide by zero!")
            printfn "%.2f / %.2f = %.2f" x y (x / y)
        with
            | :? System.DivideByZeroException -> printfn "Can't divide by zero"
    
    divide 5.0 4.0

    // Infitinty
    divide 5.0 0.0


// Define a type or Rectangle: 
type Rectangle = struct
    val Length : float
    val Width : float

    new (length, width) = 
        {Length = length; Width = width }
end

let structStuff() =
    let area (shape: Rectangle) = 
        shape.Length * shape.Width

    let rect = new Rectangle(5.0, 6.0)

    // You can use parenthesis if you want:  let rectArea = area(rect)
    // but you don't have to: let rectArea = area rect
    let rectArea = area rect

    printfn "Area: %.2f" rectArea
   


// Defining a class
type Animal = class
    val Name : string
    val Height:  float
    val Weight: float

    // constructor:
    new (name, height, weight) = 
        {
            Name = name;
            Height = height;
            Weight = weight
        }

    
    member x.Run = 
        printfn "%s is running!" x.Name
end

// inheritance:
type Dog (name, height, weight) = 
    inherit Animal(name, height, weight)

    member x.Bark = 
        printfn "%s is barking!" x.Name


let classStuff() = 
    let spot = new Animal("Spot", 3.2, 50.4)
    spot.Run

    let rover = new Dog("Rover", 2.5, 48.6)

    rover.Run
    rover.Bark
    


// Uncomment whatever you are interesting in testing!
hello() 
pitest()
paddingTest()
bindStuff()
functionsExample()
functionsExample2()
mathFunctions()
stringFunctions()
whileLoopStuff()
forLoopStuff()
conditionalStuff()
listStuff()
enumStuff()
tupleStuff()
recordStuff()
sequenceStuff()
mapStuff()
genericStuff()
expStuff()
structStuff()
classStuff()


Console.ReadKey() |> ignore