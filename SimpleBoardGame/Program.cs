using System;

namespace SimpleGame // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        // Use static array to store the map
        public static int[] Map = new int[100];

        // Use static array to store players index
        public static int[] PlayersPos = new int[2];

        // Store the player's names
        public static string[] PlayerNames = new string[2];

        // Two players flag
        static bool[] Flags = new bool[2]; // Flag[0] defualt false, and Flag [1] default false

        static void Main(string[] args)
        {
            GameShow();
            #region Get Player's Name
            Console.WriteLine("Enter player A's name: ");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == " " || PlayerNames[0] == "")
            {
                Console.WriteLine("Player A's name can't be empty");
                PlayerNames[0] = Console.ReadLine();
            }

            Console.WriteLine("Enter player B's name: ");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == " " || PlayerNames[1] == PlayerNames[0] || PlayerNames[1] == "")
            {
                if (PlayerNames[1] == " ")
                {
                    Console.WriteLine("Player B's name can't be empty");
                    PlayerNames[1] = Console.ReadLine();
                }
                else if (PlayerNames[1] == PlayerNames[0])
                {
                    Console.WriteLine("Both Players name can't be the same");
                    PlayerNames[1] = Console.ReadLine();
                }
             
            }
            #endregion
            Console.Clear(); //clear the screen
            Console.WriteLine($"{PlayerNames[0]}'s name use A");
            Console.WriteLine($"{PlayerNames[1]}'s name use B");
            InitialMap();
            DrawMap();

            // When none of the players have reached the end
            while (PlayersPos[0] < 99 && PlayersPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayerGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayersPos[0] >= 99)
                {
                    Console.WriteLine($"{PlayerNames[0]} won the game");
                }
                if (Flags[1] == false)
                {
                    PlayerGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayersPos[1] >= 99)
                {
                    Console.WriteLine($"{PlayerNames[1]} won the game");
                }
            } //while
            Console.WriteLine("See you guys next time!");
            Console.ReadKey();
        }

        /// <summary>
        /// Display the header of the game
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************");
            Console.WriteLine("****************");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("**Simple  Game**");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************");
            Console.WriteLine("****************");
        }

        /// <summary>
        /// Initial the Map by inserting the correct value
        /// </summary>
        public static void InitialMap()
        {
          
            int[] LuckyTurn = { 6, 23, 40, 55, 69, 83 };// LuckyTurn index
            foreach(int value in LuckyTurn)
            {
                Map[value] = 1;// Set all the index within the luckturn as 1
            }

            int[] Boomb = { 5, 13, 17, 33, 38, 50, 64, 80, 94 }; // Boomb index
            foreach (int value in Boomb)
            {
                Map[value] = 2;// Set all the index within the Boomb as 2
            }

            int[] Pause = { 9, 27, 60, 93 }; // Pause
            foreach (int value in Pause)
            {
                Map[value] = 3;// Set all the index within the pause as 3
            }

            int[] TimeTunnel = { 20, 25, 45, 63, 72, 88, 90 }; // Time Tunnel
            foreach (int value in TimeTunnel)
            {
                Map[value] = 4;// Set all the index within the TimeTunnel as 4
            }

        }

        /// <summary>
        /// Draw the map according to the location
        /// </summary>
        public static void DrawMap()
        {
            Console.WriteLine("# is Bomb, $ is lucky spot, ! is pause for a turn, and @ is time tunnel");
            // Draw the first map row
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }

            Console.WriteLine();

            // Draw the last column
            for(int i = 30; i < 35; i++)
            {
                for(int j = 0; j < 59; j++)
                {
                    Console.Write(" ");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }

            // Draw the second row
            for (int i = 65; i > 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();

            // Draw the first column
            for(int i = 65; i < 70; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }

            // Draw the last row
            for(int i = 70; i < 100; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
        }

        /// <summary>
        /// set each index with different char
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DrawStringMap(int i)
        {
            string str = "";
            // Players same index n on the map then draw <>
            if (PlayersPos[0] == PlayersPos[1] && PlayersPos[0] == i)
            {
                str = "<> ";
            }
            else if (PlayersPos[0] == i)
            {
                str = "A";
            }
            else if (PlayersPos[1] == i)
            {
                str = "B";
            }
            else
            {
                switch (Map[i])
                {
                    case 0:
                        str = "◻ "; // Empty sapce 
                        break;
                    case 1:
                        str = "$ "; // Luck spot 
                        break;
                    case 2:
                        str = "# "; // Boomb
                        break;
                    case 3:
                        str = "! "; // Pause Spot
                        break;
                    case 4:
                        str = "@ "; // Time Tunnel 
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// Run the game
        /// </summary>
        /// <param name="PlayerNumber"></param>
        public static void PlayerGame(int PlayerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine($"{PlayerNames[PlayerNumber]} press anykey to continue the game");
            Console.ReadKey(true); // Pressed the key won't show on the terminal
            Console.WriteLine($"{PlayerNames[PlayerNumber]} has thrown {rNumber}");
            PlayersPos[PlayerNumber] += rNumber;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine($"{PlayerNames[PlayerNumber]} press anykey to move forward");
            Console.ReadKey(true);
            Console.WriteLine($"{PlayerNames[PlayerNumber]} has moved");
            Console.ReadKey(true);

            // Player A can land on player B, empty space, lucky spot, bomb, pause and time tunnel
            if (PlayersPos[PlayerNumber] == PlayersPos[1 - PlayerNumber])
            {
                Console.WriteLine($"Player {PlayerNames[PlayerNumber]} has landed on Player {PlayerNames[1 - PlayerNumber]}. Player {PlayerNames[1 - PlayerNumber]} goes back 6 spaces!");
                PlayersPos[1 - PlayerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            // Player landed anything else
            else
            {
                // Player's position
                switch (Map[PlayersPos[PlayerNumber]])
                {
                    case 0:
                        Console.WriteLine($"Player {PlayerNames[PlayerNumber]} landed on empty spot, SAFE!!");
                        break;
                    case 1:
                        Console.WriteLine($"Player {PlayerNames[PlayerNumber]} has landed on luck spot, Pick a number: 1 - switch position 2 - Shoot another player");
                        string input = Console.ReadLine();
                        break;
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine($"Player {PlayerNames[PlayerNumber]} has chose to switch position with Player {PlayerNames[1 - PlayerNumber]}");
                                Console.ReadKey(true);
                                int temp = PlayersPos[PlayerNumber];
                                PlayersPos[PlayerNumber] = PlayersPos[1 - PlayerNumber];
                                PlayersPos[1 - PlayerNumber] = temp;
                                Console.WriteLine("Switch sucessfully! Press anykey to continue");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine($"Player {PlayersPos[PlayerNumber]} shoot Player {PlayersPos[1 - PlayerNumber]}, Player {PlayersPos[1 - PlayerNumber]} goes back 6 spaces");
                                Console.ReadKey(true);
                                PlayersPos[1 - PlayerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine($"Player {PlayerNames[1 - PlayerNumber]} has went to 6 spaces");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Make sure you only type 1 or 2");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine($"Player {PlayerNames[PlayerNumber]} landed on bomb, goes back to 6 spaces");
                        Console.ReadKey(true);
                        PlayersPos[PlayerNumber] -= 6;
                        ChangePos();
                        break;
                    case 3:
                        Console.WriteLine($"Player {PlayerNames[PlayerNumber]} has landed on pause, so pause for one turn");
                        Flags[PlayerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine($"Player {PlayerNames[PlayerNumber]} has landed on time tunnel, move forward 10 spaces");
                        PlayersPos[PlayerNumber] += 10;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                } //switch
            } //else
            ChangePos();
            Console.Clear();
            DrawMap();
        }

        /// <summary>
        /// When the player's position changes use
        /// </summary>
        public static void ChangePos()
        {
            if (PlayersPos[0] < 0)
            {
                PlayersPos[0] = 0;
            }
            if (PlayersPos[0] >= 99)
            {
                PlayersPos[0] = 99;
            }

            if (PlayersPos[1] < 0)
            {
                PlayersPos[1] = 0;
            }
            if (PlayersPos[1] >= 99)
            {
                PlayersPos[1] = 99;
            }
        }
    }
}