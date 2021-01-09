// <copyright file="Program.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Console
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Mooville.QUno.Model;

    public static class Program
    {
        public static void Main()
        {
            int humanPlayers = 0;
            int computerPlayers = 0;

            if (!Program.ParseArguments(ref humanPlayers, ref computerPlayers))
            {
                return;
            }

            if ((humanPlayers + computerPlayers) < 2)
            {
                Program.PrintUsage("There must be at least two players in the game.");
                return;
            }

            Game game = new Game();

            Program.CreatePlayers(game, humanPlayers, computerPlayers);

            try
            {
                game.Deal();
            }
            catch (InvalidOperationException)
            {
                Program.PrintUsage("There are too many players for this game.");
                return;
            }

            Console.WriteLine("Starting a new game...");

            while (!Program.IsGameOver(game))
            {
                Player player = game.CurrentPlayer;

                Card card = null;

                if (player.IsHuman)
                {
                    card = Program.AskForCardToPlay(game, player);
                }
                else
                {
                    card = Program.ChooseCardToPlay(game, player);
                }

                if (card != null)
                {
                    if (card.Color == Color.Wild)
                    {
                        Color wildColor = Color.Red;

                        if (player.IsHuman)
                        {
                            wildColor = Program.AskForWildColor();
                        }
                        else
                        {
                            wildColor = Program.ChooseWildColor(player);
                        }

                        game.PlayCard(card, wildColor);
                        Console.WriteLine(String.Format("{0} played a wild card and chose {1}.", player.Name, wildColor.ToString()));
                    }
                    else
                    {
                        game.PlayCard(card);
                        Console.WriteLine(String.Format("{0} played {1}.", player.Name, card.ToString()));
                    }
                }
                else
                {
                    Card cardToDraw = game.DrawCard();
                    player.Hand.Cards.Add(cardToDraw);
                    Console.WriteLine(String.Format("{0} drew a card.", player.Name));
                }
            }

            Console.WriteLine("Game over.");

            return;
        }

        private static void PrintVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            object[] attrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

            string copyright = String.Empty;

            if (attrs.Length > 0)
            {
                AssemblyCopyrightAttribute attr = (AssemblyCopyrightAttribute)attrs[0];
                copyright = attr.Copyright;
                copyright = copyright.Replace(@"©", @"(c)");
            }

            Console.WriteLine(String.Format("QUno, version {0}", version));
            Console.WriteLine(copyright);
            Console.WriteLine();

            return;
        }

        private static void PrintUsage(string errorMessage)
        {
            Program.PrintVersion();

            if (!String.IsNullOrEmpty(errorMessage))
            {
                Console.WriteLine(String.Format("Error: {0}", errorMessage));
                Console.WriteLine();
            }

            Console.WriteLine("Usage: QUnoConsole.exe [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -p, --human-players=<number>        The number of human (interactive) players.");
            Console.WriteLine("  -c, --computer-players=<number>     The number of computer players.");
            Console.WriteLine("  -h, --help                          Display this help message.");
            Console.WriteLine("  -v, --version                       Display the version.");
            Console.WriteLine("When starting a game, you must provide the number of human players, or the number of computer players, or both.");
            Console.WriteLine();

            return;
        }

        private static bool ParseArguments(ref int humanPlayers, ref int computerPlayers)
        {
            bool shouldContinue = true;

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    string arg = args[i];

                    if (arg.Equals("--help") || arg.Equals("-h"))
                    {
                        Program.PrintUsage(null);
                        shouldContinue = false;
                        break;
                    }

                    if (arg.Equals("--version") || arg.Equals("-v"))
                    {
                        Program.PrintVersion();
                        shouldContinue = false;
                        break;
                    }

                    string[] separators = new string[] { "=" };
                    string[] parts = arg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                    {
                        Program.PrintUsage("Those parameters are in an incorrect format.");
                        shouldContinue = false;
                        break;
                    }

                    string name = parts[0];
                    string value = parts[1];

                    if (name.Equals("--human-players") || name.Equals("-p"))
                    {
                        try
                        {
                            int number = Int32.Parse(value);
                            humanPlayers = number;
                        }
                        catch (FormatException)
                        {
                            Program.PrintUsage("That parameter is not a valid for the number of players.");
                            shouldContinue = false;
                            break;
                        }
                    }
                    else if (name.Equals("--computer-players") || name.Equals("-c"))
                    {
                        try
                        {
                            int number = Int32.Parse(value);
                            computerPlayers = number;
                        }
                        catch (FormatException)
                        {
                            Program.PrintUsage("That parameter is not a valid for the number of players.");
                            shouldContinue = false;
                            break;
                        }
                    }
                    else
                    {
                        // This block is rarely hit, because first we split the
                        // argument on = and if there isn't one, the error 
                        // is caught earlier. That could be better.
                        Program.PrintUsage("That parameter is not recognized.");
                        shouldContinue = false;
                        break;
                    }
                }
            }
            else
            {
                Program.PrintUsage(null);
                shouldContinue = false;
            }

            return shouldContinue;
        }

        private static void CreatePlayers(Game game, int humanPlayers, int computerPlayers)
        {
            bool[] humans = Enumerable.Repeat(true, humanPlayers).ToArray();
            bool[] computers = Enumerable.Repeat(false, computerPlayers).ToArray();
            bool[] players = Enumerable.Concat(humans, computers).ToArray();

            for (int i = 0; i < players.Length; i++)
            {
                string name = String.Format("Player {0}", i + 1);
                game.Players.Add(new Player() { Name = name, IsHuman = players[i] });
            }

            return;
        }

        private static void PrintGameState(Game game, Player player)
        {
            Console.WriteLine();
            Console.WriteLine(String.Format("The direction of play is {0}.", game.CurrentDirection));

            foreach (var otherPlayer in game.Players)
            {
                if (!otherPlayer.Name.Equals(player.Name))
                {
                    Console.WriteLine(String.Format("{0} has {1} cards.", otherPlayer.Name, otherPlayer.Hand.Cards.Count));
                }
            }

            Console.WriteLine(String.Format("The current card is {0}.", game.Deck.CurrentCard.ToString()));

            if (game.Deck.CurrentCard.Color == Color.Wild)
            {
                Console.WriteLine(String.Format("The current wild color is {0}.", game.Deck.CurrentWildColor));
            }

            return;
        }

        private static bool IsGameOver(Game game)
        {
            bool gameOver = game.IsGameOver;

            if (gameOver)
            {
                var player = game.Players.First(p => p.Hand.Cards.Count == 0);

                Console.WriteLine(String.Format("EPIC WIN! {0} is out of cards.", player.Name));
                Console.WriteLine();
            }

            return gameOver;
        }

        private static Card ChooseCardToPlay(Game game, Player player)
        {
            return player.ChooseCardToPlay(game);
        }

        private static Card AskForCardToPlay(Game game, Player player)
        {
            Card cardToPlay = null;

            Program.PrintGameState(game, player);

            Console.WriteLine();
            Console.WriteLine("Choose a card to play:");
            int index = 0;

            foreach (var card in player.Hand.Cards)
            {
                Console.WriteLine(String.Format("    [{0}] {1}", index, card.ToString()));
                index++;
            }

            Console.WriteLine(String.Format("    [{0}] Draw a card", index));

            string input = Console.ReadLine();

            try
            {
                int desired = Int32.Parse(input);

                if (desired < player.Hand.Cards.Count)
                {
                    Card temp = player.Hand.Cards[desired];

                    if (game.CanPlay(temp))
                    {
                        cardToPlay = temp;
                        player.Hand.Cards.RemoveAt(desired);
                    }
                    else
                    {
                        Console.WriteLine("FAIL: That card cannot play; drawing a card.");
                    }
                }
                else
                {
                    if (desired != index)
                    {
                        Console.WriteLine("FAIL: That number does not match a card; drawing a card.");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("FAIL: That input does not match a card; drawing a card.");
            }

            return cardToPlay;
        }

        private static Color ChooseWildColor(Player player)
        {
            return player.ChooseWildColor();
        }

        private static Color AskForWildColor()
        {
            Color color = Color.Red;

            Console.WriteLine();
            Console.WriteLine("Choose a wild color:");
            Console.WriteLine(String.Format("    [0] {0}", Color.Red));
            Console.WriteLine(String.Format("    [1] {0}", Color.Blue));
            Console.WriteLine(String.Format("    [2] {0}", Color.Yellow));
            Console.WriteLine(String.Format("    [3] {0}", Color.Green));

            string input = Console.ReadLine();

            try
            {
                int desired = Int32.Parse(input);

                switch (desired)
                {
                    case 0:
                        color = Color.Red;
                        break;
                    case 1:
                        color = Color.Blue;
                        break;
                    case 2:
                        color = Color.Yellow;
                        break;
                    case 3:
                        color = Color.Green;
                        break;
                    default:
                        Console.WriteLine("FAIL: That number does not match a color; choosing Red.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("FAIL: That input does not match a color; choosing Red.");
            }

            return color;
        }
    }
}
