module Mooville.QUno.Functional

open System
open System.Linq
open System.Reflection
open Microsoft.FSharp.Collections
open Mooville.QUno.Model

let getVersionAndCopyright = 
    let versionNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    let version = String.Format("QUno, version {0}", versionNumber)
    let attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof<AssemblyCopyrightAttribute>, false)
    let copyright = 
        match attributes.Length with
            | 1 -> (attributes.[0] :?> AssemblyCopyrightAttribute).Copyright
            | _ -> String.Empty
    (version, copyright)

let printUsage = 
    printfn "Usage: QUnoFunctional.exe [players] [games]"
    printfn ""
    printfn "Options:"
    printfn "  <players>                The number of players in each game."
    printfn "  <games>                  The number of games to play."
    printfn "If you do not provide options, the default number of players is 4 and the default number of games is 1."
    printfn ""
    ()

[<EntryPoint>]
let main argv =
    let (v, c) = getVersionAndCopyright
    printfn "%s" v
    printfn "%s\n" c

    let defaultNumberOfPlayers = 4
    let defaultNumberOfGames = 1

    // This is nicely concise, but will throw an exception 
    // if one of the first two arguments cannot be parsed into 
    // an integer. I struggled for a while to come up with 
    // a way to code defensively but still be functional, 
    // and I came up empty for now.
    let (numberOfPlayers, numberOfGames) = 
        match argv.Length with 
            | 0 -> (defaultNumberOfPlayers, defaultNumberOfGames)
            | 1 -> (Int32.Parse(argv.[0]), defaultNumberOfGames)
            | _ -> (Int32.Parse(argv.[0]), Int32.Parse(argv.[1]))
    
    printfn "We will play %d games with %d players each.\n" numberOfGames numberOfPlayers
    let mutable winners = []
    let games = [ 1 .. numberOfGames ]

    for g in games do
        let players = [ 1 .. numberOfPlayers ]
        let game = new Game()

        for p in players do
            let player = new Player()
            player.Name <- String.Format("Player {0}", p)
            player.IsHuman <- false
            game.Players.Add(player)
            printfn "Added %s to game #%d" player.Name g

        game.Deal()

        let mutable turnNumber = 0

        while not game.IsGameOver do
            turnNumber <- turnNumber + 1
            let currentPlayer = game.CurrentPlayer
            let currentCardName = game.Deck.CurrentCard.ToString()
            let cardToPlay = currentPlayer.ChooseCardToPlay(game)

            if cardToPlay = null then
                let cardToDraw = game.DrawCard()
                currentPlayer.Hand.Cards.Add(cardToDraw)
                printfn "[Turn %d current card is %s] %s drew a card." turnNumber currentCardName currentPlayer.Name
            else
                let cardName = cardToPlay.ToString()
                let playableCards =
                    currentPlayer.ChoosePlayableCards(game) |> Seq.toList
                    |> List.map (fun c -> c.ToString())
                    |> String.concat "|"

                if cardToPlay.Color = Color.Wild then
                    let wildColor = currentPlayer.ChooseWildColor()
                    let wildColorName = wildColor.ToString()
                    let wildColorNullable = new Nullable<Color>(wildColor)
                    game.PlayCard(cardToPlay, wildColorNullable)
                    printfn "[Turn %d current card is %s] %s played %s and chose %s. (Could have played %s)" turnNumber currentCardName currentPlayer.Name cardName wildColorName playableCards
                else
                    game.PlayCard(cardToPlay)
                    printfn "[Turn %d current card is %s] %s played %s. (Could have played %s)" turnNumber currentCardName currentPlayer.Name cardName playableCards

        let winningPlayer = game.Players.First(fun p -> p.Hand.Cards.Count = 0)
        winners <- winningPlayer.Name :: winners
        printfn "Finished game #%d. %s won!\n" g winningPlayer.Name

    let winningCounts = winners |> Seq.countBy id |> Seq.toList |> List.sort
    winningCounts |> List.iter (fun (w, n) -> printfn "%s won %d games" w n)
    printfn ""

    0 // Return an integer exit code.
